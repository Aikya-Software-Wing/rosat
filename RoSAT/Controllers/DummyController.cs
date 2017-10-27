using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoSAT.Models;

namespace RoSAT.Controllers
{
    public class DummyController : Controller
    {
        private RosatEntities db = new RosatEntities();
        // GET: Dummy
        public ActionResult Dummy()
        {
            
            List<Student> list = db.Students.ToList();
            foreach (Student s in list)
            {

                List<Mark> mlist = s.Marks.ToList();
                foreach(Mark m in mlist)
                {
                    var minMarks = db.SubjectTypes.Where(x => x.Id == m.SubType).First();
                    if (m.InternalMarks + m.ExternalMarks >= minMarks.MinMarks && m.ExternalMarks >= minMarks.MinExternalMarks)
                    {
                        m.IsPass = true;
                    }
                    else
                    {
                        m.IsPass = false;
                    }
                    db.Marks.Attach(m);
                    db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return View();
        }
    }
}