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
    public class recognitionsController : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: recognitions
        public ActionResult Index(string searchString)
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var userSearch = from u in db.recognitions select u;
                string[] userNames;
                if (!String.IsNullOrEmpty(searchString))
                {

                    userNames = searchString.Split(' '); // split the string on spaces
                    if (userNames.Count() == 1) // there is only one string so it could be
                                                // either the first or last name
                    {
                        userSearch = userSearch.Where(c => c.UserProfile.lastName.Contains(searchString) ||
                       c.UserProfile.firstName.Contains(searchString)).OrderBy(c => c.UserProfile.lastName);
                    }
                    else //if you get here there were at least two strings so extract them and test
                    {
                        string s1 = userNames[0];
                        string s2 = userNames[1];
                        userSearch = userSearch.Where(c => c.UserProfile.lastName.Contains(s2) &&
                       c.UserProfile.firstName.Contains(s1)).OrderBy(c => c.UserProfile.lastName); // note that this uses &&, not ||
                    }
                    return View(userSearch.ToList());
                }
                return View(db.recognitions.ToList());
            }
            else
            {
                return View("NotAuthorized");
            }

        }

        // GET: recognitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            return View(recognition);
        }

        // GET: recognitions/Create
        public ActionResult Create()
        {
            string empID = User.Identity.GetUserId();
            SelectList employees = new SelectList(db.userProfiles, "profileID", "fullName");
            employees = new SelectList(employees.Where(x => x.Value != empID).ToList(), "Value", "Text");
            ViewBag.profileID = employees;
            return View();
        }

        // POST: recognitions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionID,recognizerID,profileID,recognitionDescription,Now,award")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                //timestamp for recognition
                recognition.Now = DateTime.Now;

                //retrieves id of person leaving recognition
                Guid recognizerID;
                Guid.TryParse(User.Identity.GetUserId(), out recognizerID);
                recognition.recognizerID = recognizerID;

                db.recognitions.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "fullName", recognition.profileID);
            return View(recognition);
        }

        // GET: recognitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (recognition.recognizerID == memberID)
            {
                ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "fullName", recognition.profileID);
                return View(recognition);
            }
            else
            {
                return View("NotRecognitionAuthor");
            }
        }

        // POST: recognitions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,recognizerID,profileID,recognitionDescription,Now,award")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                //timestamp for recognition
                recognition.Now = DateTime.Now;

                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "fullName", recognition.profileID);
            return View(recognition);
        }

        // GET: recognitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            recognition recognition = db.recognitions.Find(id);
            if (recognition == null)
            {
                return HttpNotFound();
            }
            Guid memberID;
            Guid.TryParse(User.Identity.GetUserId(), out memberID);
            if (recognition.recognizerID == memberID)
            {
                ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "fullName", recognition.profileID);
                return View(recognition);
            }
            else
            {
                return View("NotRecognitionAuthor");
            }
        }

        // POST: recognitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            recognition recognition = db.recognitions.Find(id);
            db.recognitions.Remove(recognition);
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

        // GET: recognitions
        public ActionResult LeaderBoard()
        {
            
            var rec = db.recognitions.Include(r => r.UserProfile);
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

            return View(rec.ToList());
        }
    }
}
