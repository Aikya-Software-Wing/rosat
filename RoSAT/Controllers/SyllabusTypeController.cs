using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class SyllabusTypeController : Controller
    {
        private RosatEntities db = new RosatEntities();

       
        [HttpGet]
        public ActionResult CreateSyllabus()
        {
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            ViewBag.Year = new SelectList(db.SyllabusTypes, "Id", "Year");
            return View();
        }


        [HttpPost]
        public ActionResult CreateSyllabus(SyllabusType userInput)
        {
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();

            ViewBag.Year = new SelectList(db.SyllabusTypes, "Id", "Year");
            if (ModelState.IsValid)
            {
                TempData["studentId"] = student.Id;
                TempData["syllabusId"] = db.SyllabusTypes.Where(x => x.Id == userInput.Year).First().Id;
                TempData["isCGPA"] = db.SyllabusTypes.Where(x => x.Id == userInput.Year).First().IsCGPA;
                TempData["semester"] = 1;
                return RedirectToAction("CreateMark", "Mark");
            }

            return View(userInput);
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