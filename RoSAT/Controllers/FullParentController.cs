using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
//using System.Linq;
using System.Net;

namespace RoSAT.Controllers
{
    public class FullParentController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: FullParent/Create
        public ActionResult Create()
        {
            ViewBag.SalaryType = new SelectList(db.SalaryTypes, "Id", "Name");
            return View();
        }

        // POST: FullParent/Create
        [HttpPost]
        public ActionResult Create(SecondParent collection)
        {
            Student student = db.Students.Find(TempData.Peek("StudentId"));
            ViewBag.SalaryType = new SelectList(db.SalaryTypes, "Id", "Name");

            if (collection.FatherPhone == student.PhoneNumber)
            {
                ModelState.AddModelError("FatherPhone", "Father's phone can not be same as student's phone");
                return View(collection);
            }

            if (collection.MotherPhone == student.PhoneNumber)
            {
                ModelState.AddModelError("MotherPhone", "Mother's phone can not be same as student's phone");
                return View(collection);
            }

            if (ModelState.IsValid)
            {
                student.Parents.Add(new Parent
                {
                    Id = Guid.NewGuid(),
                    Name = collection.FathersName,
                    Qualification = collection.FatherQualification,
                    Occupation = collection.FatherOccupation,
                    EmailId = collection.FatherEmail,
                    SalaryType = collection.FatherSalary,
                    PhoneNo = collection.FatherPhone,
                    PType = 1
                });

                student.Parents.Add(new Parent
                {
                    Id = Guid.NewGuid(),
                    Name = collection.MothersName,
                    Qualification = collection.MotherQualification,
                    Occupation = collection.MotherOccupation,
                    EmailId = collection.MotherEmail,
                    SalaryType = collection.MotherSalary,
                    PhoneNo = collection.MotherPhone,
                    PType = 2
                });


                db.Students.Attach(student);
                db.Entry(student).State = EntityState.Modified;
                TempData["studentId"] = student.Id;
                db.SaveChanges();

                return RedirectToAction("Create", "Addresses");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            Student student = db.Students.Find(TempData.Peek("StudentId"));
            ViewBag.SalaryType = new SelectList(db.SalaryTypes, "Id", "Name");
            try
            {
                SecondParent parent = new SecondParent
                {
                    FatherEmail = student.Parents.Where(x => x.PType == 1).First().EmailId,
                    FatherOccupation = student.Parents.Where(x => x.PType == 1).First().Occupation,
                    FatherPhone = student.Parents.Where(x => x.PType == 1).First().PhoneNo.Value,
                    FatherQualification = student.Parents.Where(x => x.PType == 1).First().Qualification,
                    FatherSalary = student.Parents.Where(x => x.PType == 1).First().SalaryType.Value,
                    FathersName = student.Parents.Where(x => x.PType == 1).First().Name,
                    MotherEmail = student.Parents.Where(x => x.PType == 2).First().EmailId,
                    MotherOccupation = student.Parents.Where(x => x.PType == 2).First().Occupation,
                    MotherPhone = student.Parents.Where(x => x.PType == 2).First().PhoneNo.Value,
                    MotherQualification = student.Parents.Where(x => x.PType == 2).First().Qualification,
                    MotherSalary = student.Parents.Where(x => x.PType == 2).First().SalaryType.Value,
                    MothersName = student.Parents.Where(x => x.PType == 2).First().Name
                };

                return View(parent);
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Create");
            }
        }

        [HttpPost]
        public ActionResult Edit(SecondParent parent)
        {
            Student student = db.Students.Find(TempData.Peek("StudentId"));
            ViewBag.SalaryType = new SelectList(db.SalaryTypes, "Id", "Name");

            if (parent.FatherPhone == student.PhoneNumber)
            {
                ModelState.AddModelError("FatherPhone", "Father's phone can not be same as student's phone");
                return View(parent);
            }

            if (parent.MotherPhone == student.PhoneNumber)
            {
                ModelState.AddModelError("MotherPhone", "Mother's phone can not be same as student's phone");
                return View(parent);
            }

            if (ModelState.IsValid)
            {
                Parent father = student.Parents.Where(x => x.PType == 1).First();
                father.Name = parent.FathersName;
                father.Qualification = parent.FatherQualification;
                father.Occupation = parent.FatherOccupation;
                father.EmailId = parent.FatherEmail;
                father.SalaryType = parent.FatherSalary;
                father.PhoneNo = parent.FatherPhone;

                db.Parents.Attach(father);
                db.Entry(father).State = EntityState.Modified;
                db.SaveChanges();

                Parent mother = student.Parents.Where(x => x.PType == 2).First();
                mother.Name = parent.MothersName;
                mother.Qualification = parent.MotherQualification;
                mother.Occupation = parent.MotherOccupation;
                mother.EmailId = parent.MotherEmail;
                mother.SalaryType = parent.MotherSalary;
                mother.PhoneNo = parent.MotherPhone;

                db.Parents.Attach(mother);
                db.Entry(mother).State = EntityState.Modified;
                db.SaveChanges();

                TempData["studentId"] = student.Id;

                
                return RedirectToAction("Edit", "Addresses");
            }

            return View(parent);
        }
    }
}
