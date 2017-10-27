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
    public class SubjectTypesController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: SubjectTypes
        public ActionResult Index()
        {
            var subjectTypes = db.SubjectTypes.Include(s => s.Department).Include(s => s.SyllabusType);
            return View(subjectTypes.ToList());
        }

        // GET: SubjectTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectType subjectType = db.SubjectTypes.Find(id);
            if (subjectType == null)
            {
                return HttpNotFound();
            }
            return View(subjectType);
        }

        // GET: SubjectTypes/Create
        public ActionResult Create()
        {
            ViewBag.Dept = new SelectList(db.Departments, "Id", "Name");
            ViewBag.SubId = new SelectList(db.SyllabusTypes, "Id", "Id");
            return View();
        }

        // POST: SubjectTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Semester,Name,Code,IsLab,MinMarks,MinExternalMarks,SubId,MaxCredits,Dept")] SubjectType subjectType)
        {
            if (ModelState.IsValid)
            {
                db.SubjectTypes.Add(subjectType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Dept = new SelectList(db.Departments, "Id", "Name", subjectType.Dept);
            ViewBag.SubId = new SelectList(db.SyllabusTypes, "Id", "Id", subjectType.SubId);
            return View(subjectType);
        }

        // GET: SubjectTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectType subjectType = db.SubjectTypes.Find(id);
            if (subjectType == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dept = new SelectList(db.Departments, "Id", "Name", subjectType.Dept);
            ViewBag.SubId = new SelectList(db.SyllabusTypes, "Id", "Id", subjectType.SubId);
            return View(subjectType);
        }

        // POST: SubjectTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Semester,Name,Code,IsLab,MinMarks,MinExternalMarks,SubId,MaxCredits,Dept")] SubjectType subjectType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Dept = new SelectList(db.Departments, "Id", "Name", subjectType.Dept);
            ViewBag.SubId = new SelectList(db.SyllabusTypes, "Id", "Id", subjectType.SubId);
            return View(subjectType);
        }

        // GET: SubjectTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectType subjectType = db.SubjectTypes.Find(id);
            if (subjectType == null)
            {
                return HttpNotFound();
            }
            return View(subjectType);
        }

        // POST: SubjectTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectType subjectType = db.SubjectTypes.Find(id);
            db.SubjectTypes.Remove(subjectType);
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
