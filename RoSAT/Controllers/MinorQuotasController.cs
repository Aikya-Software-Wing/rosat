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
    public class MinorQuotasController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: MinorQuotas
        public ActionResult Index()
        {
            return View(db.MinorQuotas.ToList());
        }

        // GET: MinorQuotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorQuota minorQuota = db.MinorQuotas.Find(id);
            if (minorQuota == null)
            {
                return HttpNotFound();
            }
            return View(minorQuota);
        }

        // GET: MinorQuotas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MinorQuotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MinorQuotas")] MinorQuota minorQuota)
        {
            if (ModelState.IsValid)
            {
                db.MinorQuotas.Add(minorQuota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(minorQuota);
        }

        // GET: MinorQuotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorQuota minorQuota = db.MinorQuotas.Find(id);
            if (minorQuota == null)
            {
                return HttpNotFound();
            }
            return View(minorQuota);
        }

        // POST: MinorQuotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MinorQuotas")] MinorQuota minorQuota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(minorQuota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(minorQuota);
        }

        // GET: MinorQuotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorQuota minorQuota = db.MinorQuotas.Find(id);
            if (minorQuota == null)
            {
                return HttpNotFound();
            }
            return View(minorQuota);
        }

        // POST: MinorQuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MinorQuota minorQuota = db.MinorQuotas.Find(id);
            db.MinorQuotas.Remove(minorQuota);
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
