using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class MarkController : Controller
    {
        private RosatEntities db = new RosatEntities();

        [HttpGet]
        public ActionResult CreateMark()
        {
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            int semester = (int)TempData.Peek("semester");
            int syllabusId = (int)TempData.Peek("syllabusId");

            var subjectList = db.SubjectTypes.Where(x => x.Dept == student.Department && x.Semester == semester && x.SubId == syllabusId).ToList();
            var failedSubjectList = student.Marks.Where(x => x.Sem == (semester - 1) && x.IsPass == false).ToList();

            List<Mark> marksToEnter = new List<Mark>();

            foreach (var subject in subjectList)
            {
                if (student.Marks.Where(x => x.Sem == semester && x.SylType == syllabusId && x.SubType == subject.Id).Count() > 0)
                {
                    var marks = student.Marks.Where(x => x.Sem == semester && x.SylType == syllabusId && x.SubType == subject.Id).First();
                    marksToEnter.Add(new Mark
                    {
                        Id = marks.Id,
                        Sem = semester,
                        ExternalMarks = marks.ExternalMarks,
                        InternalMarks = marks.InternalMarks,
                        SubjectType = marks.SubjectType,
                        SubType = marks.SubType,
                        SylType = marks.SubType
                    });
                }
                else
                {
                    if (subject.SyllabusType.IsCGPA)
                    {
                        marksToEnter.Add(new Mark
                        {
                            Id = Guid.Empty,
                            Sem = semester,
                            SubjectType = subject,
                            InternalMarks = 0,
                            SubType = subject.Id,
                            SylType = syllabusId
                        });
                    }
                    else
                    {
                        marksToEnter.Add(new Mark
                        {
                            Id = Guid.Empty,
                            Sem = semester,
                            SubjectType = subject,
                            SubType = subject.Id,
                            SylType = syllabusId
                        });
                    }
                }
            }

            foreach (var mark in failedSubjectList)
            {
                var exsisting = student.Marks.Where(x => x.Sem == semester && x.SubType == mark.SubType && x.SylType == mark.SylType).ToList();
                if (exsisting.Count > 0)
                {
                    mark.Id = exsisting[0].Id;
                    mark.Sem = semester;
                    mark.InternalMarks = exsisting[0].InternalMarks;
                    mark.ExternalMarks = exsisting[0].ExternalMarks;
                    marksToEnter.Add(mark);
                }
                else
                {
                    mark.Id = Guid.Empty;
                    mark.Sem = semester;
                    mark.InternalMarks = null;
                    mark.ExternalMarks = null;
                    marksToEnter.Add(mark);
                }
            }


            return View(marksToEnter.ToArray());
        }


        [HttpPost]
        public ActionResult CreateMark(Mark[] userInput)
        {
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            int semester = (int)TempData.Peek("semester");
            int syllabusId = (int)TempData.Peek("syllabusId");

            foreach (var mark in userInput)
            {
                if (syllabusId == 1)
                {
                    if (mark.InternalMarks > 25 || mark.ExternalMarks > 100)
                    {
                        ModelState.AddModelError("", "Invalid Marks");
                        return RedirectToAction("CreateMark");
                    }
                }
                else
                {
                }

            }

            if (ModelState.IsValid)
            {
                foreach (var mark in userInput)
                {
                    if (mark.Id == Guid.Empty)
                    {
                        mark.Id = Guid.NewGuid();
                        mark.StudentId = student.Id;

                        var minMarks = db.SubjectTypes.Where(x => x.Id == mark.SubType).First();
                        if (mark.InternalMarks + mark.ExternalMarks >= minMarks.MinMarks && mark.ExternalMarks >= minMarks.MinExternalMarks)
                        {
                            mark.IsPass = true;
                        }
                        else
                        {
                            mark.IsPass = false;
                        }

                        db.Marks.Add(mark);
                        db.SaveChanges();
                    }
                    else
                    {
                        var orginal = student.Marks.Where(x => x.Id == mark.Id).First();
                        orginal.InternalMarks = mark.InternalMarks;
                        orginal.ExternalMarks = mark.ExternalMarks;
                        orginal.Sem = semester;

                        var minMarks = db.SubjectTypes.Where(x => x.Id == mark.SubType).First();
                        if (orginal.InternalMarks + orginal.ExternalMarks >= minMarks.MinMarks && orginal.ExternalMarks >= minMarks.MinExternalMarks)
                        {
                            orginal.IsPass = true;
                        }
                        else
                        {
                            orginal.IsPass = false;
                        }

                        db.Marks.Attach(orginal);
                        db.Entry(orginal).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                int semster = (int)TempData.Peek("semester") + 1;
                TempData["semester"] = semster;
                if (semster < student.Semester)
                {
                    return RedirectToAction("CreateMark");
                }
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("CreateMark");
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