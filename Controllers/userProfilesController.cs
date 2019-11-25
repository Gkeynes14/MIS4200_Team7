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

            if (User.Identity.IsAuthenticated)
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
            else
            {
                return View("NotAuthorized");
            }
            
             
    }

        // GET: userProfiles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userProfile = db.userProfiles.Find(id);
            var rec = db.recognitions.Where(r => r.profileID == id);
            var recList = rec.ToList();
            ViewBag.rec = recList;

            //calculations for leadboard
            var totalCnt = recList.Count(); //counts all the recognitions for that person
            var rec1Cnt = recList.Where(r => r.award == recognition.CoreValue.Excellence).Count();
            // counts all the Excellence recognitions
            // notice how the Enum values are references, class.enum.value
            // the next two lines show another way to do the same counting
            var rec2Cnt = recList.Count(r => r.award == recognition.CoreValue.Culture);
            var rec3Cnt = recList.Count(r => r.award == recognition.CoreValue.Integrity);
            var rec4Cnt = recList.Count(r => r.award == recognition.CoreValue.Stewardship);
            var rec5Cnt = recList.Count(r => r.award == recognition.CoreValue.Innovate);
            var rec6Cnt = recList.Count(r => r.award == recognition.CoreValue.Passion);
            var rec7Cnt = recList.Count(r => r.award == recognition.CoreValue.Balance);
            // copy the values into the ViewBag
            ViewBag.total = totalCnt;
            ViewBag.Excellence = rec1Cnt;
            ViewBag.Culture = rec2Cnt;
            ViewBag.Integrity = rec3Cnt;
            ViewBag.Stewardship = rec4Cnt;
            ViewBag.Innovate = rec5Cnt;
            ViewBag.Passion = rec6Cnt;
            ViewBag.Balance = rec7Cnt;

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
