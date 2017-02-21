using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Kramer.Domain;
using Kramer.Helpers;
using Kramer.Models;
using Kramer.Repository;
using Kramer.Repository.Interfaces;
using Kramer.Services;
using Microsoft.AspNet.Identity;
using Kramer.Services.Interfaces;

namespace Kramer.Controllers
{
    [Authorize]
    public class UserRequestsController : Controller
    {
        private ISaleTypeRepository saleTypeRepository;
        private IUserRequestRepository userRequestRepository;
        private IUserRepository userRepository;
        private ISaleTypeService saleTypeService;
        private IStatusRepository statusRepository;
        private INotificationService notificationService;

        public UserRequestsController(
            IUserRequestRepository userRequestRepository, 
            ISaleTypeRepository saleTypeRepository, 
            IUserRepository userRepository,
            IStatusRepository statusRepository,
            ISaleTypeService saleTypeService,
            INotificationService notificationService)
        {
            this.userRequestRepository = userRequestRepository;
            this.saleTypeRepository = saleTypeRepository;
            this.userRepository = userRepository;
            this.saleTypeService = saleTypeService;
            this.statusRepository = statusRepository;
            this.notificationService = notificationService;
        }


        // GET: UserRequests
        public ActionResult Index()
        {
            var currentUser = GetCurrentUser();
            ViewBag.UserIsAdmin = UserIsAdmin(currentUser);

            var requests = userRequestRepository.All(); //select * from UserRequest

            if (!UserIsAdmin(currentUser))
            {
                //select * fromUserRequest 
                //where requestById == currentUser.Id
                requests = requests.Where(request => request.RequestedBy.Id == currentUser.Id);
            }

            return View(requests.ToList());
        }

        // GET: UserRequests/Create
        public ActionResult Create()
        {
            var viewModel = new UserRequestFormViewModel();
            viewModel.AvailableSaleTypes = GetSaleTypes();
            return View(viewModel);
        }

        // POST: UserRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRequestFormViewModel userRequest)
        {
            const int PENDING = 1;

            if (ModelState.IsValid && ValidateSaleTypeForCurrentUser(userRequest))
            {
                var dbModelUser = Mapper.Map<UserRequest>(userRequest);

                dbModelUser.SaleType = GetSaleTypeById(userRequest.SaleType.Id);
                dbModelUser.RequestedBy = GetCurrentUser();
                dbModelUser.StatusId = PENDING;

                userRequestRepository.Add(dbModelUser);
                return RedirectToAction("Index");
            }

            return View(userRequest);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChangeStatus(int id)
        {
            var userRequest = Mapper.Map<UserRequestChangeStatusViewModel>(userRequestRepository.GetById(id));
            userRequest.Statuses = Mapper.Map<List<StatusViewModel>>(statusRepository.All().ToList());
            return View(userRequest);
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult ChangeStatus(UserRequestChangeStatusViewModel userRequestViewModel)
        {
            const int CANCELED = 3;
            const int COMPLETED = 2;

            var userRequest = userRequestRepository.GetById(userRequestViewModel.Id);
            Mapper.Map<UserRequestChangeStatusViewModel, UserRequest>(userRequestViewModel, userRequest);
            userRequestRepository.Update(userRequest);

            if (userRequestViewModel.Status.Id == COMPLETED)
            {
                notificationService.SendConfirmationToRequester(userRequest.RequestedBy.Email, userRequest.RequestedBy.Name, userRequest.Email);
                notificationService.SendCredentialsToUser(userRequest.Email, userRequest.Name, userRequest.Username, userRequestViewModel.Password);
            }
            else if(userRequestViewModel.Status.Id == CANCELED)
            {
                notificationService.SendCancellationToRequester(userRequest.RequestedBy.Email, userRequest.RequestedBy.Name, userRequest.Email);
            }


            return RedirectToAction("Index");
        }

        //O edit está com problemas de conversão de datetime2 pra datetime
        //Possibilidade de editar uma solicitação de outro usuário através da url
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userRequest = GetUserRequestForCurrentUser(id);
            if (userRequest == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<UserRequestFormViewModel>(userRequest);
            model.AvailableSaleTypes = GetSaleTypes();
            return View(model);
        }

        private UserRequest GetUserRequestForCurrentUser(int id)
        {
            var currentUser = GetCurrentUser();
            var currentUserIsAdmin = UserIsAdmin(currentUser);

            //Se o usuário for admin, ele pode ter acesso a qualquer UserRequest criado.
            UserRequest userRequest =
                userRequestRepository.All()
                    .FirstOrDefault(
                        request => request.Id == id && (currentUserIsAdmin || request.RequestedBy.Id == currentUser.Id));
            return userRequest;
        }

        // POST: UserRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRequestFormViewModel userRequest)
        {
            if (!ValidateSaleTypeForCurrentUser(userRequest))
                return View(userRequest);

            var dbModel = GetUserRequestForCurrentUser(userRequest.Id);
            if(dbModel == null)
                return View(userRequest);
            
            Mapper.Map(userRequest, dbModel);
            var userSaleType = userRequest.SaleType.Id;
            if(userSaleType != null)
                dbModel.SaleTypeId = userSaleType;
            userRequestRepository.Update(dbModel);
            return RedirectToAction("Index");
        }

        private bool ValidateSaleTypeForCurrentUser(UserRequestFormViewModel userRequest)
        {
            return userRequest.SaleType == null || GetSaleTypes().Any(saleType => saleType.Id == userRequest.SaleType.Id);
        }

        private ApplicationUser GetCurrentUser()
        {
            return userRepository.GetById(User.Identity.GetUserId());
        }

        private bool UserIsAdmin(ApplicationUser user)
        {
            const string ADMIN = "1";

            return user.Roles.Any(role => role.RoleId == ADMIN);
        }

        private List<SaleTypeViewModel> GetSaleTypes()
        {
            var saleTypes = saleTypeService.GetAvailableSaleTypes(GetCurrentUser().Id);
            return Mapper.Map<List<SaleTypeViewModel>>(saleTypes);
        }

        private SaleType GetSaleTypeById(int id)
        {
            return saleTypeRepository.GetById(id);
        }
    }
}
