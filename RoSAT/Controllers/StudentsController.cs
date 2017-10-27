using System;
using System.Web.Mvc;
using RoSAT.Models;
using System.Web;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data.Entity;

namespace RoSAT.Controllers
{
    public class StudentsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Gender = new SelectList(db.Genders, "Id", "Name");
            ViewBag.AdmissionQuota = new SelectList(db.Quotas, "Id", "Name");
            ViewBag.MajorQuota = new SelectList(db.MajorQuotas, "Id", "Name");
            ViewBag.MinorQuota = new SelectList(db.MinorQuotas, "Id", "Name");
            Student student = new Student();
            student.AdmissionQuota = 38;
            return View(student);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name", student.Department);
            ViewBag.Gender = new SelectList(db.Genders, "Id", "Name", student.Gender);
            ViewBag.AdmissionQuota = new SelectList(db.Quotas, "Id", "Name", student.AdmissionQuota);
            ViewBag.MajorQuota = new SelectList(db.MajorQuotas, "Id", "Name",student.MajorQuota);
            ViewBag.MinorQuota = new SelectList(db.MinorQuotas, "Id", "Name",student.MinorQuota);

            if (db.Students.Where(x => x.Usn == student.Usn).Count() > 0)
            {
                ModelState.AddModelError("Usn", "Usn already registered");
                return View(student);
            }



            student.EmailId = User.Identity.Name;
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                HttpPostedFileBase file = Request.Files["photoUpload"];
                if (file.ContentLength != 0)
                {
                    if (Helpers.HttpPostedFileBaseExtensions.IsImage(file))
                    {
                        if (file.ContentLength > 5e+6)
                        {
                            ModelState.AddModelError("photo", "Photo too large");
                            return View(student);
                        }
                        else
                        {
                            byte[] temp = new byte[file.ContentLength];
                            file.InputStream.Read(temp, 0, file.ContentLength);
                            student.photo = temp;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("photo", "Please upload a valid photo");
                        return View(student);
                    }
                }

                db.Students.Add(student);
                db.SaveChanges();
                TempData["StudentId"] = student.Id;
                return RedirectToAction("Create", "FullParent");
            }
            return View(student);
        }

        public ActionResult ShowPhoto()
        {
            string email = User.Identity.Name;
            Student stud = db.Students.Where(x => x.EmailId == email).First();
            ViewBag.ByteArray =stud.photo;
            return View("Photo");
        }

        private int GetMajorFromAdmission(string[] Quota)
        {
            List<MajorQuota> allmajor = db.MajorQuotas.ToList();
            foreach (var major in allmajor)
            {
                if (Regex.IsMatch(major.Name, Quota[0], RegexOptions.IgnoreCase))
                    return major.Id;
            }
            return 1;
        }
        private int GetMinorFromAdmission(string[] Quota)
        {
            List<MinorQuota> allminor = db.MinorQuotas.ToList();
            foreach (var minor in allminor)
            {
                for(int i=1;i<Quota.Length-1;i++)
                    if (Regex.IsMatch(minor.Name,Quota[i], RegexOptions.IgnoreCase))
                        return minor.Id;
            }
            return 1;
        }


        [HttpGet]
        public ActionResult Edit()
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Gender = new SelectList(db.Genders, "Id", "Name");
            ViewBag.AdmissionQuota = new SelectList(db.Quotas, "Id", "Name");
            ViewBag.MajorQuota = new SelectList(db.MajorQuotas, "Id", "Name");
            ViewBag.MinorQuota = new SelectList(db.MinorQuotas, "Id", "Name");
            string email = User.Identity.Name;
            Student student = db.Students.Where(x => x.EmailId == email).First();
            if(student.AdmissionQuota != 38)
            {
                Quota Quotas = db.Quotas.Where(x => x.Id == student.AdmissionQuota).First();
                student.MajorQuota = GetMajorFromAdmission(Quotas.Name.Split());
                student.MinorQuota = GetMinorFromAdmission(Quotas.Name.Split());
                db.Students.Attach(student);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "Name", student.Department);
            ViewBag.Gender = new SelectList(db.Genders, "Id", "Name", student.Gender);
            ViewBag.AdmissionQuota = new SelectList(db.Quotas, "Id", "Name", student.AdmissionQuota);
            ViewBag.MajorQuota = new SelectList(db.MajorQuotas, "Id", "Name", student.MajorQuota);
            ViewBag.MinorQuota = new SelectList(db.MinorQuotas, "Id", "Name", student.MinorQuota);

            student.EmailId = User.Identity.Name;
            
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["photoUpload"];
                if (file.ContentLength != 0)
                {
                    if (Helpers.HttpPostedFileBaseExtensions.IsImage(file))
                    {
                        if (file.ContentLength > 5e+6)
                        {
                            ModelState.AddModelError("photo", "Photo too large");
                            return View(student);
                        }
                        else
                        {
                            byte[] temp = new byte[file.ContentLength];
                            file.InputStream.Read(temp, 0, file.ContentLength);
                            student.photo = temp;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("photo", "Please upload a valid photo");
                        return View(student);
                    }
                }

                db.Students.Attach(student);
                db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["StudentId"] = student.Id;
                return RedirectToAction("Edit", "FullParent");
            }
            return View(student);
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
