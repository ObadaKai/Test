using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ta3lim.Database;

namespace Ta3lim.Controllers
{
    public class DailyReportController : Controller
    {
        TaalimEntities db = new TaalimEntities();
        // GET: DailyReport
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"];
                var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Basics == true)
                {
                    return View(db.DailyActivities.ToList());
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"];
                var id = Convert.ToInt32(Session["ID"]);
                var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Teaching == true || type.Guidence == true)
                {
                    if (!(db.DailyActivities.OrderByDescending(x=>x.Employee.id == id).FirstOrDefault().date == DateTime.Now.Date))
                    {
                        return View();
                    }

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Create(DailyActivity daily)
        {
            try
            {
                daily.id = db.DailyActivities.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                daily.id = 1;
            }
            if (ModelState.IsValid)
            {
                daily.Managerid = Convert.ToInt32(Session["ID"]);
                daily.date = DateTime.Now.Date;
                db.DailyActivities.Add(daily);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}