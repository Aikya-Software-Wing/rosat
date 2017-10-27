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
    public class ParentTypesController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: ParentTypes
        public ActionResult Index()
        {
            return View(db.ParentTypes.ToList());
        }

        // GET: ParentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentType parentType = db.ParentTypes.Find(id);
            if (parentType == null)
            {
                return HttpNotFound();
            }
            return View(parentType);
        }

        // GET: ParentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] ParentType parentType)
        {
            if (ModelState.IsValid)
            {
                db.ParentTypes.Add(parentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parentType);
        }

        // GET: ParentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentType parentType = db.ParentTypes.Find(id);
            if (parentType == null)
            {
                return HttpNotFound();
            }
            return View(parentType);
        }

        // POST: ParentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] ParentType parentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parentType);
        }

        // GET: ParentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParentType parentType = db.ParentTypes.Find(id);
            if (parentType == null)
            {
                return HttpNotFound();
            }
            return View(parentType);
        }

        // POST: ParentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParentType parentType = db.ParentTypes.Find(id);
            db.ParentTypes.Remove(parentType);
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
