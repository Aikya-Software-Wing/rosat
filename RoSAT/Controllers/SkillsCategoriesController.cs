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
    public class SkillsCategoriesController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: SkillsCategories
        public ActionResult Index()
        {
            return View(db.SkillsCategories.ToList());
        }

        // GET: SkillsCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillsCategory skillsCategory = db.SkillsCategories.Find(id);
            if (skillsCategory == null)
            {
                return HttpNotFound();
            }
            return View(skillsCategory);
        }

        // GET: SkillsCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillsCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SkillsCategory skillsCategory)
        {
            if (ModelState.IsValid)
            {
                db.SkillsCategories.Add(skillsCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skillsCategory);
        }

        // GET: SkillsCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillsCategory skillsCategory = db.SkillsCategories.Find(id);
            if (skillsCategory == null)
            {
                return HttpNotFound();
            }
            return View(skillsCategory);
        }

        // POST: SkillsCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SkillsCategory skillsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skillsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skillsCategory);
        }

        // GET: SkillsCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillsCategory skillsCategory = db.SkillsCategories.Find(id);
            if (skillsCategory == null)
            {
                return HttpNotFound();
            }
            return View(skillsCategory);
        }

        // POST: SkillsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SkillsCategory skillsCategory = db.SkillsCategories.Find(id);
            db.SkillsCategories.Remove(skillsCategory);
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
