using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using RoSAT.Models;

namespace RoSAT.Controllers
{
    public class AreaInterestsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        // GET: AreaInterests/Create
        public ActionResult Create()
        {
            if (TempData.Peek("AreaList") == null)
            {
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                if (student.AreaInterests.Count > 0)
                {
                    TempData["AreaList"] = student.AreaInterests.ToList();
                }
            }
            return View();
        }

        // POST: AreaInterests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,Area")] AreaInterest areaInterest)
        {
            if (ModelState.IsValid)
            {
                List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
                areaInterest.Id = Guid.NewGuid();
                areaList.Add(areaInterest);
                TempData["AreaList"] = areaList;
            }
            return View();
        }

        public ActionResult ListArea()
        {
            List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
            return PartialView("_ListArea", areaList);
        }

        // GET: AreaInterests/Edit/5
        public ActionResult Edit(Guid? id)
        {
            List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
            return View(areaList.Where(x => x.Id == id).First());
        }

        // POST: AreaInterests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AreaInterest userInput)
        {
            if (!ModelState.IsValid)
            {
                return View(userInput);
            }

            List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
            areaList.Remove(areaList.Where(x => x.Id == userInput.Id).First());
            areaList.Add(userInput);
            TempData["AreaList"] = areaList;
            return RedirectToAction("Create");
        }

        // GET: AreaInterests/Delete/5
        public ActionResult Delete(Guid? id)
        {
            List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
            areaList.Remove(areaList.Where(x => x.Id == id).First());
            TempData["AreaList"] = areaList;
            return RedirectToAction("Create");
        }

        public ActionResult Proceed()
        {
            List<AreaInterest> areaList = TempData.Peek("AreaList") == null ? new List<AreaInterest>() : (List<AreaInterest>)TempData.Peek("AreaList");
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            var studentArea = student.AreaInterests;
            for (int i = 0; i < studentArea.Count; i++)
            {
                var currentArea = student.AreaInterests.ToArray()[i];
                if (areaList.Where(x => x.Id == currentArea.Id).Count() == 0)
                {
                    db.Entry(currentArea).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }

            foreach (AreaInterest areaInterest in areaList)
            {
                if (db.AreaInterests.Find(areaInterest.Id) != null)
                {
                    AreaInterest sh = db.AreaInterests.Find(areaInterest.Id);
                    sh.Area = areaInterest.Area;
                    db.AreaInterests.Attach(sh);
                    db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    student.AreaInterests.Add(areaInterest);
                    db.SaveChanges();
                }
            }

            TempData["studentId"] = student.Id;

            TempData.Remove("AreaList");

            //if ((bool)TempData["isEditing"])
            //{
            //    TempData.Remove("isEditing");
            //    return RedirectToAction("CreateScheme", "Scheme");
            //}

            return RedirectToAction("CreateSyllabus", "SyllabusType");
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
