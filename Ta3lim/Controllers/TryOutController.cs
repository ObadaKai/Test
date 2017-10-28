using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ta3lim.Database;

namespace Ta3lim.Controllers
{
    public class TryOutController : Controller
    {
        // GET: TryOut
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApStudent(Student st)
        {
           return RedirectToAction("Index", "Students");
        }
    }
}