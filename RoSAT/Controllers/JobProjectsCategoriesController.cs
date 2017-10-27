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
    public class JobProjectsCategoriesController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: JobProjectsCategories
        public ActionResult Index()
        {
            return View(db.JobProjectsCategories.ToList());
        }

        // GET: JobProjectsCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobProjectsCategory jobProjectsCategory = db.JobProjectsCategories.Find(id);
            if (jobProjectsCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobProjectsCategory);
        }

        // GET: JobProjectsCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobProjectsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] JobProjectsCategory jobProjectsCategory)
        {
            if (ModelState.IsValid)
            {
                db.JobProjectsCategories.Add(jobProjectsCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobProjectsCategory);
        }

        // GET: JobProjectsCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobProjectsCategory jobProjectsCategory = db.JobProjectsCategories.Find(id);
            if (jobProjectsCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobProjectsCategory);
        }

        // POST: JobProjectsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] JobProjectsCategory jobProjectsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobProjectsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobProjectsCategory);
        }

        // GET: JobProjectsCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobProjectsCategory jobProjectsCategory = db.JobProjectsCategories.Find(id);
            if (jobProjectsCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobProjectsCategory);
        }

        // POST: JobProjectsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobProjectsCategory jobProjectsCategory = db.JobProjectsCategories.Find(id);
            db.JobProjectsCategories.Remove(jobProjectsCategory);
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
