using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class SkillsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        [HttpGet]
        public ActionResult CreateSkill()
        {
            if (TempData.Peek("SkillList") == null)
            {
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                if (student.Skills.Count > 0)
                {
                    TempData["SkillList"] = db.SubSkills.Where(x => x.Skill.StudentId == student.Id).ToList();
                }
            }

            ViewBag.Category = new SelectList(db.SkillsCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateSkill(SubSkill userInput)
        {
            ViewBag.Category = new SelectList(db.SkillsCategories, "Id", "Name");

            if (ModelState.IsValid)
            {
                List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
                userInput.Id = Guid.NewGuid();
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                var skillCount = student.Skills.Where(x => x.Category == userInput.Category).ToList().Count;
                if (skillCount > 0)
                {
                    userInput.Skill = student.Skills.Where(x => x.Category == userInput.Category).First();
                }
                else
                {
                    Skill skill = new Skill
                    {
                        Id = Guid.NewGuid(),
                        Category = userInput.Category
                    };
                    userInput.Skill = skill;
                }
                userInput.Skill.SkillsCategory = db.SkillsCategories.Where(x => x.Id == userInput.Category).First();
                HttpPostedFileBase file = Request.Files["photoUpload"];
                if (file.ContentLength != 0)
                {
                    if (Helpers.HttpPostedFileBaseExtensions.IsImage(file))
                    {
                        if (file.ContentLength > 5e+6)
                        {
                            ModelState.AddModelError("CertificatePhoto", "Photo too large");
                            return View(userInput);
                        }
                        else
                        {
                            byte[] temp = new byte[file.ContentLength];
                            file.InputStream.Read(temp, 0, file.ContentLength);
                            userInput.CertificatePhoto = temp;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CertificatePhoto", "Please upload a valid photo");
                        return View(userInput);
                    }
                }
                skillList.Add(userInput);
                TempData["SkillList"] = skillList;
            }

            return View();
        }

        public ActionResult ListSkills()
        {
            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            return PartialView("_ListSkills", skillList);
        }

        public ActionResult ViewCertificate(Guid id)
        {
            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            SubSkill skill = skillList.Where(x => x.Id == id).First();
            ViewBag.ByteArray = skill.CertificatePhoto;
            return View("Photo");
        }

        [HttpGet]
        public ActionResult EditSkill(Guid id)
        {
            ViewBag.Category1 = new SelectList(db.SkillsCategories, "Id", "Name");
            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            return View(skillList.Where(x => x.Id == id).First());
        }

        [HttpPost]
        public ActionResult EditSkill(SubSkill userInput)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = new SelectList(db.SkillsCategories, "Id", "Name");
                return View(userInput);
            }

            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            skillList.Remove(skillList.Where(x => x.Id == userInput.Id).First());
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            var skillCount = student.Skills.Where(x => x.Category == userInput.Category).ToList().Count;
            if (skillCount > 0)
            {
                userInput.Skill = student.Skills.Where(x => x.Category == userInput.Category).First();
            }
            else
            {
                Skill skill = new Skill
                {
                    Id = Guid.NewGuid(),
                    Category = userInput.Category
                };
                userInput.Skill = skill;
            }
            userInput.Skill.SkillsCategory = db.SkillsCategories.Where(x => x.Id == userInput.Category).First();
            HttpPostedFileBase file = Request.Files["photoUpload"];
            if (file.ContentLength != 0)
            {
                if (Helpers.HttpPostedFileBaseExtensions.IsImage(file))
                {
                    if (file.ContentLength > 5e+6)
                    {
                        ModelState.AddModelError("CertificatePhoto", "Photo too large");
                        return View(userInput);
                    }
                    else
                    {
                        byte[] temp = new byte[file.ContentLength];
                        file.InputStream.Read(temp, 0, file.ContentLength);
                        userInput.CertificatePhoto = temp;
                    }
                }
                else
                {
                    ModelState.AddModelError("CertificatePhoto", "Please upload a valid photo");
                    return View(userInput);
                }
            }
            skillList.Add(userInput);
            TempData["SkillList"] = skillList;
            return RedirectToAction("CreateSkill");
        }

        [HttpGet]
        public ActionResult DeleteSkill(Guid id)
        {
            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            skillList.Remove(skillList.Where(x => x.Id == id).First());
            TempData["SkillList"] = skillList;
            return RedirectToAction("CreateSkill");
        }

        public ActionResult Proceed()
        {
            List<SubSkill> skillList = TempData.Peek("SkillList") == null ? new List<SubSkill>() : (List<SubSkill>)TempData.Peek("SkillList");
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();

            var studentSkills = student.Skills.ToArray();
            for (int i = 0; i < studentSkills.Length; i++)
            {
                var studentSubSkills = studentSkills[i].SubSkills.ToArray();
                for (int j = 0; j < studentSubSkills.Length; j++)
                {
                    var currentSubSkill = studentSubSkills[j];
                    if (skillList.Where(x => x.Id == currentSubSkill.Id).Count() == 0)
                    {
                        db.Entry(currentSubSkill).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
            }

            foreach (SubSkill subskill in skillList)
            {
                if (db.SubSkills.Find(subskill.Id) != null)
                {
                    SubSkill sh = db.SubSkills.Find(subskill.Id);
                    var skillCount = student.Skills.Where(x => x.Category == subskill.Skill.SkillsCategory.Id).ToList().Count;
                    if (skillCount > 0)
                    {
                        var skill = student.Skills.Where(x => x.Category == subskill.Skill.SkillsCategory.Id).First();
                        sh.Name = subskill.Name;
                        sh.CertificateId = subskill.CertificateId;
                        sh.CertificateEarnedDate = subskill.CertificateEarnedDate;
                        sh.CertificateExpiryDate = subskill.CertificateExpiryDate;
                        sh.CertificateIssuingAuthority = subskill.CertificateIssuingAuthority;
                        sh.CertificatePhoto = subskill.CertificatePhoto;
                        sh.Skill = skill;
                        db.SubSkills.Attach(sh);
                        db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        Skill skill = new Skill
                        {
                            Id = Guid.NewGuid(),
                            Category = subskill.Category
                        };
                        skill.Student = student;
                        sh.Skill = skill;
                        sh.Name = subskill.Name;
                        sh.CertificateId = subskill.CertificateId;
                        sh.CertificateEarnedDate = subskill.CertificateEarnedDate;
                        sh.CertificateExpiryDate = subskill.CertificateExpiryDate;
                        sh.CertificateIssuingAuthority = subskill.CertificateIssuingAuthority;
                        sh.CertificatePhoto = subskill.CertificatePhoto;
                        db.SubSkills.Attach(sh);
                        db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var skillCount = student.Skills.Where(x => x.Category == subskill.Skill.SkillsCategory.Id).ToList().Count;
                    if (skillCount > 0)
                    {
                        student.Skills.Where(x => x.Category == subskill.Skill.SkillsCategory.Id).First().SubSkills.Add(new SubSkill
                        {
                            Id = Guid.NewGuid(),
                            Name = subskill.Name,
                            CertificateId = subskill.CertificateId,
                            CertificateEarnedDate = subskill.CertificateEarnedDate,
                            CertificateExpiryDate = subskill.CertificateExpiryDate,
                            CertificateIssuingAuthority = subskill.CertificateIssuingAuthority,
                            CertificatePhoto = subskill.CertificatePhoto
                        });

                        db.Students.Attach(student);
                        db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        Skill skill = new Skill
                        {
                            Id = Guid.NewGuid(),
                            Category = subskill.Category
                        };
                        SubSkill subSkill = new SubSkill();
                        subSkill.Id = Guid.NewGuid();
                        subSkill.Name = subskill.Name;
                        subSkill.CertificateId = subskill.CertificateId;
                        subSkill.CertificateEarnedDate = subskill.CertificateEarnedDate;
                        subSkill.CertificateExpiryDate = subskill.CertificateExpiryDate;
                        subSkill.CertificateIssuingAuthority = subskill.CertificateIssuingAuthority;
                        subSkill.CertificatePhoto = subskill.CertificatePhoto;

                        skill.SubSkills.Add(subSkill);

                        student.Skills.Add(skill);

                        db.Students.Attach(student);
                        db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }

            TempData["studentId"] = student.Id;

           

            TempData.Remove("SkillList");
            return RedirectToAction("CreateJobProject", "JobProject");
        }


        public ActionResult Confirmation()
        {
            return View();
        }
    }
}