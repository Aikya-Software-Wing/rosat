using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class JobProjectController : Controller
    {
        private RosatEntities db = new RosatEntities();

        [HttpGet]
        public ActionResult CreateJobProject()
        {
            if (TempData.Peek("ProjectList") == null)
            {
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                if (student.JobProjects.Count > 0)
                {
                    TempData["ProjectList"] = student.JobProjects.ToList();
                }
            }
            ViewBag.Category = new SelectList(db.JobProjectsCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateJobProject(JobProject userInput)
        {
            ViewBag.Category = new SelectList(db.JobProjectsCategories, "Id", "Name");
            if (ModelState.IsValid)
            {
                List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
                userInput.JobProjectsCategory = db.JobProjectsCategories.Where(x => x.Id == userInput.Category).First();
                userInput.Id = Guid.NewGuid();
                projectList.Add(userInput);
                TempData["ProjectList"] = projectList;
            }
            return View();
        }

        public ActionResult ListProject()
        {
            List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
            return PartialView("_ListProject", projectList);
        }

        [HttpGet]
        public ActionResult EditJobProject(Guid id)
        {
            ViewBag.Category1 = new SelectList(db.JobProjectsCategories, "Id", "Name");
            List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
            return View(projectList.Where(x => x.Id == id).First());
        }

        [HttpPost]
        public ActionResult EditJobProject(JobProject userInput)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category1 = new SelectList(db.JobProjectsCategories, "Id", "Name");
                return View(userInput);
            }

            List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
            projectList.Remove(projectList.Where(x => x.Id == userInput.Id).First());
            userInput.JobProjectsCategory = db.JobProjectsCategories.Where(x => x.Id == userInput.Category).First();
            projectList.Add(userInput);
            TempData["ProjectList"] = projectList;
            return RedirectToAction("CreateJobProject");

        }

        [HttpGet]
        public ActionResult DeleteJobProject(Guid id)
        {
            List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
            projectList.Remove(projectList.Where(x => x.Id == id).First());
            TempData["ProjectList"] = projectList;
            return RedirectToAction("CreateJobProject");
        }

        public ActionResult Proceed()
        {
            List<JobProject> projectList = TempData.Peek("ProjectList") == null ? new List<JobProject>() : (List<JobProject>)TempData.Peek("ProjectList");
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            var studentJobProject = student.JobProjects;
            for (int i = 0; i < studentJobProject.Count; i++)
            {
                var currentJobProject = student.JobProjects.ToArray()[i];
                if (projectList.Where(x => x.Id == currentJobProject.Id).Count() == 0)
                {
                    db.Entry(currentJobProject).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }

            foreach (JobProject jobProject in projectList)
            {
                if (db.JobProjects.Find(jobProject.Id) != null)
                {
                    JobProject sh = db.JobProjects.Find(jobProject.Id);
                    sh.Name = jobProject.Name;
                    sh.Descri = jobProject.Descri;
                    sh.Duration = jobProject.Duration;
                    sh.Salary = jobProject.Salary;
                    sh.Category = jobProject.Category;
                    db.JobProjects.Attach(sh);
                    db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    jobProject.JobProjectsCategory = null;
                    student.JobProjects.Add(jobProject);
                    db.SaveChanges();
                }
            }

            TempData["studentId"] = student.Id;

            TempData.Remove("ProjectList");
            return RedirectToAction("Create", "AreaInterests");
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