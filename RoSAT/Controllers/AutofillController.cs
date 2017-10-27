using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class AutofillController : Controller
    {
        private RosatEntities db = new RosatEntities();
        // GET: Autofill
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
                for (int i = 1; i < Quota.Length - 1; i++)
                    if (Regex.IsMatch(minor.Name, Quota[i], RegexOptions.IgnoreCase))
                        return minor.Id;
            }
            return 3;
        }
        public ActionResult Index()
        {
            List<Student> students = db.Students.ToList();
            foreach(var student in students)
            if (student.AdmissionQuota != 38)
            {
                Quota Quotas = db.Quotas.Where(x => x.Id == student.AdmissionQuota).First();
                student.MajorQuota = GetMajorFromAdmission(Quotas.Name.Split());
                student.MinorQuota = GetMinorFromAdmission(Quotas.Name.Split());
                db.Students.Attach(student);
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }
    }
}