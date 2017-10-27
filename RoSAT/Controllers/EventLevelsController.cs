using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RoSAT.Models;

namespace RoSAT.Controllers
{
    [Authorize(Roles ="admin")]
    public class EventLevelsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: EventLevels
        public ActionResult Index()
        {
            return View(db.EventLevels.ToList());
        }

        // GET: EventLevels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLevel eventLevel = db.EventLevels.Find(id);
            if (eventLevel == null)
            {
                return HttpNotFound();
            }
            return View(eventLevel);
        }

        // GET: EventLevels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventLevels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] EventLevel eventLevel)
        {
            if (ModelState.IsValid)
            {
                db.EventLevels.Add(eventLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventLevel);
        }

        // GET: EventLevels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLevel eventLevel = db.EventLevels.Find(id);
            if (eventLevel == null)
            {
                return HttpNotFound();
            }
            return View(eventLevel);
        }

        // POST: EventLevels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] EventLevel eventLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventLevel);
        }

        // GET: EventLevels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventLevel eventLevel = db.EventLevels.Find(id);
            if (eventLevel == null)
            {
                return HttpNotFound();
            }
            return View(eventLevel);
        }

        // POST: EventLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventLevel eventLevel = db.EventLevels.Find(id);
            db.EventLevels.Remove(eventLevel);
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
