using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class SchoolsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        [HttpGet]
        public ActionResult CreateSchool()
        {
            if (TempData.Peek("SchoolList") == null)
            {
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                if (student.Schools.Count > 0)
                {
                    TempData["SchoolList"] = student.Schools.ToList();
                }
            }

            ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
            ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateSchool(School userInput)
        {
            ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
            ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");

            if(userInput.IsGPA && (userInput.PercentageMarks < 0 || userInput.PercentageMarks > 10))
            {
                ModelState.AddModelError("PercentageMarks", "GPA not in range");
                ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
                ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
                return View(userInput);
            }

           else if (userInput.PercentageMarks < 0 || userInput.PercentageMarks > 100)
            {
                ModelState.AddModelError("PercentageMarks", "Percentage not in range");
                ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
                ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
                return View(userInput);
            }

            if (ModelState.IsValid)
            {
                List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
                userInput.BoardType = db.BoardTypes.Where(x => x.Id == userInput.Board).First();
                userInput.SchoolType = db.SchoolTypes.Where(x => x.Id == userInput.SchoolTypeId).First();
                userInput.Id = Guid.NewGuid();
                if (userInput.IsGPA)
                {
                    userInput.PercentageMarks = userInput.PercentageMarks * new decimal(9.5);
                }
                schoolList.Add(userInput);
                TempData["SchoolList"] = schoolList;
            }
            return View();

        }

        public ActionResult ListSchools()
        {
            List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
            return PartialView("_ListSchools", schoolList);
        }

        [HttpGet]
        public ActionResult EditSchool(Guid id)
        {
            ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
            ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");

            List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
            return View(schoolList.Where(x => x.Id == id).First());
        }

        [HttpPost]
        public ActionResult EditSchool(School userInput)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
                ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
                return View(userInput);
            }
            if (userInput.IsGPA && (userInput.PercentageMarks < 0 || userInput.PercentageMarks > 10))
            {
                ModelState.AddModelError("PercentageMarks", "GPA not in range");
                ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
                ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
                return View(userInput);
            }

            else if (userInput.PercentageMarks < 0 || userInput.PercentageMarks > 100)
            {
                ModelState.AddModelError("PercentageMarks", "Percentage not in range");
                ViewBag.Board = new SelectList(db.BoardTypes, "Id", "Name");
                ViewBag.Types = new SelectList(db.SchoolTypes, "Id", "Name");
                return View(userInput);
            }

            

           

            List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
            schoolList.Remove(schoolList.Where(x => x.Id == userInput.Id).First());
            userInput.BoardType = db.BoardTypes.Where(x => x.Id == userInput.Board).First();
            userInput.SchoolType = db.SchoolTypes.Where(x => x.Id == userInput.SchoolTypeId).First();
            
            if (userInput.IsGPA)
            {
                userInput.PercentageMarks = userInput.PercentageMarks * new decimal(9.5);
            }
            schoolList.Add(userInput);
            TempData["SchoolList"] = schoolList;
            return RedirectToAction("CreateSchool");

        }

        [HttpGet]
        public ActionResult DeleteSchool(Guid id)
        {
            List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
            schoolList.Remove(schoolList.Where(x => x.Id == id).First());
            TempData["SchoolList"] = schoolList;
            return RedirectToAction("CreateSchool");
        }

        public ActionResult Proceed()
        {
            List<School> schoolList = TempData.Peek("SchoolList") == null ? new List<School>() : (List<School>)TempData.Peek("SchoolList");
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            var studentSchools = student.Schools;
            for (int i = 0; i < studentSchools.Count; i++)
            {
                var currentSchool = student.Schools.ToArray()[i];
                if (schoolList.Where(x => x.Id == currentSchool.Id).Count() == 0)
                {
                    db.Entry(currentSchool).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }

            foreach (School school in schoolList)
            {
                if (db.Schools.Find(school.Id) != null)
                {
                    School sh = db.Schools.Find(school.Id);
                    sh.IsGPA = school.IsGPA;
                    sh.Name = school.Name;
                    if (school.IsGPA)
                    {
                        sh.PercentageMarks = school.PercentageMarks;
                    }
                    else
                    {
                        sh.PercentageMarks = school.PercentageMarks;
                    }
                    sh.MediumInstruction = school.MediumInstruction;
                    sh.Board = school.Board;
                    sh.IsUrban = school.IsUrban;
                    sh.SchoolTypeId = school.SchoolTypeId;
                    db.Schools.Attach(sh);
                    db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    school.SchoolType = null;
                    school.BoardType = null;
                    student.Schools.Add(school);
                    db.SaveChanges();
                }
            }

            TempData["studentId"] = student.Id;

            TempData.Remove("SchoolList");
            return RedirectToAction("CreateEvent", "Events");
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