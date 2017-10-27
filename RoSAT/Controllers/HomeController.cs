using RoSAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoSAT.Controllers
{
    public class HomeController : Controller
    {
        RosatEntities db = new RosatEntities();

        public ActionResult Index()
        {
            ViewBag.hasRegistered = db.Students.Where(x => x.EmailId == User.Identity.Name).Count() == 1;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}