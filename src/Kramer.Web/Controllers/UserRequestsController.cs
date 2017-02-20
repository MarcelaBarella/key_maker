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
            const string ADMIN = "1";

            var currentUser = GetCurrentUser();
            bool isAdmin = currentUser.Roles.Any(role => role.RoleId == ADMIN);
            var requests = userRequestRepository.All(); //select * from UserRequest

            if (!isAdmin)
            {
                //select * fromUserRequest 
                //where requestById == currentUser.Id
                requests = requests.Where(request => request.RequestedBy.Id == currentUser.Id);
            }

            return View(requests.ToList());
        }

        // GET: UserRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRequest userRequest = userRequestRepository.GetById(id.Value);
            if (userRequest == null)
            {
                return HttpNotFound();
            }
            return View(userRequest);
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

            if (ModelState.IsValid)
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
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            UserRequest userRequest = userRequestRepository.GetById(id);
            if (userRequest == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<UserRequestFormViewModel>(userRequest);
            model.AvailableSaleTypes = GetSaleTypes();
            return View(model);
        }

        // POST: UserRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRequestFormViewModel userRequest)
        {
            var dbModel = userRequestRepository.GetById(userRequest.Id);
            
            Mapper.Map(userRequest, dbModel);
            var userSaleType = userRequest.SaleType.Id;
            if(userSaleType != null)
                dbModel.SaleTypeId = userSaleType;
            userRequestRepository.Update(dbModel);
            return RedirectToAction("Index");
        }

        private ApplicationUser GetCurrentUser()
        {
            return userRepository.GetById(User.Identity.GetUserId());
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
