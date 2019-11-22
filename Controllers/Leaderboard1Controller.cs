﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIS4200_Team7.DAL;
using MIS4200_Team7.Models;

namespace MIS4200_Team7.Controllers
{
    public class Leaderboard1Controller : Controller
    {
        private MIS4200Context db = new MIS4200Context();

        // GET: Leaderboard1
        public ActionResult Index()
        {
            var recognitions = db.recognitions.Include(r => r.UserProfile).Include(r => r.Value);
            return View(recognitions.ToList());
        }

        // GET: Leaderboard1/Details/5
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

        // GET: Leaderboard1/Create
        public ActionResult Create()
        {
            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "email");
            ViewBag.valueID = new SelectList(db.values, "valueID", "valueName");
            return View();
        }

        // POST: Leaderboard1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recognitionID,recognizerID,profileID,valueID,recognitionDescription,Now")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.recognitions.Add(recognition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "email", recognition.profileID);
            ViewBag.valueID = new SelectList(db.values, "valueID", "valueName", recognition.valueID);
            return View(recognition);
        }

        // GET: Leaderboard1/Edit/5
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
            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "email", recognition.profileID);
            ViewBag.valueID = new SelectList(db.values, "valueID", "valueName", recognition.valueID);
            return View(recognition);
        }

        // POST: Leaderboard1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recognitionID,recognizerID,profileID,valueID,recognitionDescription,Now")] recognition recognition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recognition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.profileID = new SelectList(db.userProfiles, "profileID", "email", recognition.profileID);
            ViewBag.valueID = new SelectList(db.values, "valueID", "valueName", recognition.valueID);
            return View(recognition);
        }

        // GET: Leaderboard1/Delete/5
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
            return View(recognition);
        }

        // POST: Leaderboard1/Delete/5
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
    }
}
