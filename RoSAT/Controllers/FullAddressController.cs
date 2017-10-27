using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class FullAddressController : Controller
    {
        private RosatEntities db = new RosatEntities();
        // GET: FullAddress
        public ActionResult Index()
        {
            return View();
        }

        // GET: FullAddress/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FullAddress/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FullAddress/Create
        [HttpPost]
        public ActionResult Create(SecondAddress collection)
        {
            Student student = db.Students.Find(TempData.Peek("StudentId"));
            if (ModelState.IsValid)
            {

                student.Addresses.Add(new Address
                {
                    Addr = collection.PermanentAddress,
                    AType = 1
                });

                student.Addresses.Add(new Address
                {
                    Addr = collection.PresentAddress,
                    AType = 2
                });

                db.Students.Attach(student);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create","FullParent");

            };

            return View();
            
           
            
        }

        // GET: FullAddress/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FullAddress/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FullAddress/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FullAddress/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
