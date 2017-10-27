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
    public class MajorQuotasController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: MajorQuotas
        public ActionResult Index()
        {
            return View(db.MajorQuotas.ToList());
        }

        // GET: MajorQuotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorQuota majorQuota = db.MajorQuotas.Find(id);
            if (majorQuota == null)
            {
                return HttpNotFound();
            }
            return View(majorQuota);
        }

        // GET: MajorQuotas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MajorQuotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MajorQuotas")] MajorQuota majorQuota)
        {
            if (ModelState.IsValid)
            {
                db.MajorQuotas.Add(majorQuota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(majorQuota);
        }

        // GET: MajorQuotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorQuota majorQuota = db.MajorQuotas.Find(id);
            if (majorQuota == null)
            {
                return HttpNotFound();
            }
            return View(majorQuota);
        }

        // POST: MajorQuotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MajorQuotas")] MajorQuota majorQuota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorQuota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(majorQuota);
        }

        // GET: MajorQuotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorQuota majorQuota = db.MajorQuotas.Find(id);
            if (majorQuota == null)
            {
                return HttpNotFound();
            }
            return View(majorQuota);
        }

        // POST: MajorQuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MajorQuota majorQuota = db.MajorQuotas.Find(id);
            db.MajorQuotas.Remove(majorQuota);
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
