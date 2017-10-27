using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace RoSAT.Controllers
{
    public class EventsController : Controller
    {
        private RosatEntities db = new RosatEntities();

        [HttpGet]
        public ActionResult CreateEvent()
        {
            if (TempData.Peek("EventList") == null)
            {
                Guid studentId = (Guid)TempData.Peek("studentId");
                Student student = db.Students.Where(x => x.Id == studentId).First();
                if (student.Events.Count > 0)
                {
                    TempData["EventList"] = student.Events.ToList();
                }
            }

            ViewBag.EventLevel = new SelectList(db.EventLevels, "Id", "Name");
            ViewBag.Category = new SelectList(db.EventTypes, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateEvent(Event userInput)
        {
            ViewBag.EventLevel = new SelectList(db.EventLevels, "Id", "Name");
            ViewBag.Category = new SelectList(db.EventTypes, "Id", "Name");
            if (ModelState.IsValid)
            {
                List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
                userInput.EventLevel = db.EventLevels.Where(x => x.id == userInput.ELevel).First();
                userInput.EventType = db.EventTypes.Where(x => x.Id == userInput.Category).First();
                userInput.Id = Guid.NewGuid();
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
                eventList.Add(userInput);
                TempData["EventList"] = eventList;
            }
            return View();
        }

        public ActionResult ListEvent()
        {
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
            return PartialView("_ListEvent", eventList);
        }

        [HttpGet]
        public ActionResult EditEvent(Guid id)
        {
            ViewBag.EventLevel = new SelectList(db.EventLevels, "Id", "Name");
            ViewBag.Category = new SelectList(db.EventTypes, "Id", "Name");
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");

            return View(eventList.Where(x => x.Id == id).First());
        }

        [HttpPost]
        public ActionResult EditEvent(Event userInput)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EventLevel = new SelectList(db.EventLevels, "Id", "Name");
                ViewBag.Category = new SelectList(db.EventTypes, "Id", "Name");
                return View(userInput);
            }

            
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
            eventList.Remove(eventList.Where(x => x.Id == userInput.Id).First());
            userInput.EventLevel = db.EventLevels.Where(x => x.id == userInput.ELevel).First();
            userInput.EventType = db.EventTypes.Where(x => x.Id == userInput.Category).First();
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
            eventList.Add(userInput);
            TempData["EventList"] = eventList;
            return RedirectToAction("CreateEvent");

        }

        [HttpGet]
        public ActionResult DeleteEvent(Guid id)
        {
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
            eventList.Remove(eventList.Where(x => x.Id == id).First());
            TempData["EventList"] = eventList;
            return RedirectToAction("CreateEvent");
        }

        public ActionResult ViewCertificate(Guid id)
        {
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
            Event myevent = eventList.Where(x => x.Id == id).First();
            ViewBag.ByteArray = myevent.CertificatePhoto;
            return View("Photo");
        }

        public ActionResult Proceed()
        {
            List<Event> eventList = TempData.Peek("EventList") == null ? new List<Event>() : (List<Event>)TempData.Peek("EventList");
            Guid studentId = (Guid)TempData.Peek("studentId");
            Student student = db.Students.Where(x => x.Id == studentId).First();
            var studentEvent = student.Events;
            for (int i = 0; i < studentEvent.Count; i++)
            {
                var currentEvent = student.Events.ToArray()[i];
                if (eventList.Where(x => x.Id == currentEvent.Id).Count() == 0)
                {
                    db.Entry(currentEvent).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }

            foreach (Event event1 in eventList)
            {
                if (db.Events.Find(event1.Id) != null)
                {
                    Event sh = db.Events.Find(event1.Id);
                    sh.Name = event1.Name;
                    sh.Position = event1.Position;
                    sh.ELevel = event1.ELevel;
                    sh.Category = event1.Category;
                    sh.CertificateId = event1.CertificateId;
                    sh.CertificateEarnedDate = event1.CertificateEarnedDate;
                    sh.CertificateIssuingAuthority = event1.CertificateIssuingAuthority;
                    sh.CertificatePhoto = event1.CertificatePhoto;

                    db.Events.Attach(sh);
                    db.Entry(sh).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    event1.EventLevel = null;
                    event1.EventType = null;                    
                    student.Events.Add(event1);
                    db.SaveChanges();
                }
            }

            TempData["studentId"] = student.Id;

            TempData.Remove("EventList");
            return RedirectToAction("CreateSkill", "Skills");
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