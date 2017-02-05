﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kramer.Infra;
using Kramer.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace Kramer.Controllers
{
    [Authorize]
    public class UserRequestsController : Controller
    {
        private ApplicationDbContext db;

        public UserRequestsController(ApplicationDbContext database)
        {
            this.db = database;
        }

        // GET: UserRequests
        public ActionResult Index()
        {
            const string ADMIN = "1";

            var currentUser = GetCurrentUser();
            bool isAdmin = currentUser.Roles.Any(role => role.RoleId == ADMIN);
            var requests = db.UserRequest.AsQueryable(); //select * from UserRequest

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
            UserRequest userRequest = db.UserRequest.Find(id);
            if (userRequest == null)
            {
                return HttpNotFound();
            }
            return View(userRequest);
        }

        // GET: UserRequests/Create
        public ActionResult Create()
        {
            return View(new UserRequestFormViewModel());
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
                var dbModel = Mapper.Map<UserRequest>(userRequest);
                dbModel.RequestedBy = GetCurrentUser();
                dbModel.Pending = true;

                db.UserRequest.Add(dbModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRequest);
        }

        private ApplicationUser GetCurrentUser()
        {
            return db.Users.Find(User.Identity.GetUserId());
        }

        [Authorize(Roles="Admin")]
        public ActionResult Edit(int? id)
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

            var model = Mapper.Map<UserRequestFormViewModel>(userRequest);
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

            db.Entry(dbModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserRequests/Delete/5
        public ActionResult Delete(int? id)
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
        }

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
