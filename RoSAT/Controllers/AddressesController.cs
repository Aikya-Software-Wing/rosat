using System;
using System.Data.Entity;
using System.Web.Mvc;
using RoSAT.Models;
using System.Linq;

namespace RoSAT.Controllers
{
    public class AddressesController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                Address presentAddress = new Address
                {
                    Id = Guid.NewGuid(),
                    AType = 2,
                    Addr = address.presentAddress
                };
                Address permanenetAddress = new Address
                {
                    Id = Guid.NewGuid(),
                    AType = 1,
                    Addr = address.permanentAddress
                };

                Student student = db.Students.Find(TempData.Peek("StudentId"));
                student.Addresses.Add(presentAddress);
                student.Addresses.Add(permanenetAddress);

                db.Students.Attach(student);
                db.Entry(student).State = EntityState.Modified;
                TempData["studentId"] = student.Id;

                db.SaveChanges();
                return RedirectToAction("CreateSchool", "Schools");
            }


            return View(address);

        }

        [HttpGet]
        public ActionResult Edit()
        {
            Student student = db.Students.Find(TempData.Peek("StudentId"));

            try
            {
                AddressViewModel model = new AddressViewModel
                {
                    permanentAddress = student.Addresses.Where(x => x.AType == 1).First().Addr,
                    presentAddress = student.Addresses.Where(x => x.AType == 2).First().Addr
                };
                return View(model);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Create");
            }

        }

        [HttpPost]
        public ActionResult Edit(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student student = db.Students.Find(TempData.Peek("StudentId"));

                Address present = student.Addresses.Where(x => x.AType == 2).First();
                Address permanent = student.Addresses.Where(x => x.AType == 1).First();

                present.Addr = model.presentAddress;
                db.Addresses.Attach(present);
                db.Entry(present).State = EntityState.Modified;
                db.SaveChanges();

                permanent.Addr = model.permanentAddress;
                db.Addresses.Attach(permanent);
                db.Entry(permanent).State = EntityState.Modified;
                db.SaveChanges();

                TempData["studentId"] = student.Id;
                TempData["isEditing"] = true;
                db.SaveChanges();
                return RedirectToAction("CreateSchool", "Schools");
            }

            return View();
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
