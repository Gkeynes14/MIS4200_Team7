using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MIS4200_Team7.DAL;
using MIS4200_Team7.Models;

namespace MIS4200_Team7.Controllers
{
    public class userProfilesController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: userProfiles
        public ActionResult Index(string searchString)
        {
            var testusers = from u in db.userProfiles select u;
             if (!String.IsNullOrEmpty(searchString))
                 {
                testusers = testusers.Where(u =>
               u.lastName.Contains(searchString)
               || u.firstName.Contains(searchString));
                // if here, users were found so view them
                return View(testusers.ToList());
                 }
            return View(db.userProfiles.ToList());
             
    }

        // GET: userProfiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // GET: userProfiles/Create
        public ActionResult Create()
        {
            ViewBag.positionID = new SelectList(db.positions, "positionID", "positionTitle");
            return View();
        }

        // POST: userProfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "profileID,email,firstName,lastName,hireDate,positionID")] userProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                //link from register to create user profile
                Guid memberID;
                Guid.TryParse(User.Identity.GetUserId(), out memberID);
                userProfile.profileID = memberID;
                db.userProfiles.Add(userProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
                
                
            }

            ViewBag.positionID = new SelectList(db.positions, "positionID", "positionTitle", userProfile.positionID);
            return View(userProfile);
        }

        // GET: userProfiles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userProfile userProfile = db.userProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.positionID = new SelectList(db.positions, "positionID", "positionTitle", userProfile.positionID);
            return View(userProfile);
        }

        // POST: userProfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "profileID,email,firstName,lastName,hireDate,positionID")] userProfile userProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.positionID = new SelectList(db.positions, "positionID", "positionTitle", userProfile.positionID);
            return View(userProfile);
        }

        // GET: userProfiles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userProfile userProfile = db.userProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            return View(userProfile);
        }

        // POST: userProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            userProfile userProfile = db.userProfiles.Find(id);
            db.userProfiles.Remove(userProfile);
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
