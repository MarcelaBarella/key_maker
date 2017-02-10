using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kramer.Domain;
using Kramer.Models;
using AutoMapper;
using Kramer.Repository;
using Microsoft.AspNet.Identity;

namespace Kramer.Controllers
{
    [Authorize]
    public class UserRequestsController : Controller
    {
        private ISaleTypeRepository saleTypeRepository;
        private IUserRequestRepository userRequestRepository;
        
        public UserRequestsController(IUserRequestRepository userRequestRepository, ISaleTypeRepository saleTypeRepository)
        {
            this.userRequestRepository = userRequestRepository;
            this.saleTypeRepository = saleTypeRepository;
            //this.db = db;
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
            if (ModelState.IsValid)
            {

                var dbModelUser = Mapper.Map<UserRequest>(userRequest);

                dbModelUser.SaleType = GetSaleTypeById(userRequest.SaleType.Id);
                dbModelUser.RequestedBy = GetCurrentUser();
                dbModelUser.Pending = true;

                userRequestRepository.Add(dbModelUser);
                return RedirectToAction("Index");
            }

            return View(userRequest);
        }

        private ApplicationUser GetCurrentUser()
        {
            return db.Users.Find(User.Identity.GetUserId());
        }

        private  List<SaleTypeViewModel>GetSaleTypes()
        {
            return Mapper.Map<List<SaleTypeViewModel>>(db.SaleType.ToList());
        }

        private SaleType GetSaleTypeById(int id)
        {
            return .SaleType.FirstOrDefault(_ => _.Id == id);
        }

        [Authorize(Roles="Admin")]
        public ActionResult ChangeStatus(UserRequestFormViewModel userRequest)
        {
            userRequestRepository.ChangeStatus(userRequest.Id);
            return RedirectToAction("Index");
        }

        
        public ActionResult Edit(int? id)
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
            var dbModel = db.UserRequest.Find(userRequest.Id);
            
            Mapper.Map(userRequest, dbModel);

            dbModel.SaleTypeId = userRequest.SaleType.Id;
            userRequestRepository.Update(dbModel);
            return RedirectToAction("Index");
        }

        // GET: UserRequests/Delete/5
        /*public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRequest userRequest = db.UserRequest.Find(id);
            if (userRequest == null)
            {
                return HttpNotFound();
            }
            return View(userRequest);
        }

        // POST: UserRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRequest userRequest = db.UserRequest.Find(id);
            db.UserRequest.Remove(userRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
