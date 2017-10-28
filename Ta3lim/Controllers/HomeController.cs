using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ta3lim.Database;

namespace Ta3lim.Controllers
{
    public class HomeController : Controller
    {
        private TaalimEntities db = new TaalimEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Username ,string Password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = db.Employees.Where(x => x.Username == Username && x.Password == Password).FirstOrDefault();
                    Session["ID"] = employee.id;
                    Session["Type"] = employee.EmployeeType.Type;
                    Session["Basics"] = employee.EmployeeType.Basics;
                    Session["Managment"] = employee.EmployeeType.Managment;
                    Session["Observing"] = employee.EmployeeType.Observing;
                    Session["Guidence"] = employee.EmployeeType.Guidence;
                    Session["Teaching"] = employee.EmployeeType.Teaching;
                    return RedirectToAction("Create", "DailyReport");
                }
                catch (Exception) { }
            }
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Type"] = null;
            Session["Basics"] = null;
            Session["Managment"] = null;
            Session["Observing"] = null;
            Session["Guidence"] = null;
            Session["Teaching"] = null;
            return RedirectToAction("Index");
        }

    }
}