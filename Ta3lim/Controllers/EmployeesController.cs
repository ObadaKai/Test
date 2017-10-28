using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ta3lim.Database;

namespace Ta3lim.Controllers
{
    public class EmployeesController : Controller
    {
        private TaalimEntities db = new TaalimEntities();

        // GET: Employees
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true)
                {

                    var employees = db.Employees.Include(e => e.Center).Include(e => e.EmployeeType).Include(e => e.Period);
                    return View(employees.ToList());
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true)
                {

                    Employee employee = db.Employees.Find(id);
                    if (employee == null)
                    {
                        return HttpNotFound();
                    }
                    return View(employee);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {

                    ViewBag.Centerid = new SelectList(db.Centers, "id", "Name");
                    ViewBag.Job = new SelectList(db.EmployeeTypes.Where(x => x.Basics != true && x.Observing != true), "id", "Type");
                    ViewBag.Periodid = new SelectList(db.Periods, "id", "Name");
                    return View();

                }
                if (type.Basics == true)
                {

                    ViewBag.Centerid = new SelectList(db.Centers, "id", "Name");
                    ViewBag.Job = new SelectList(db.EmployeeTypes.Where(x => x.Guidence != true && x.Teaching != true), "id", "Type");
                    ViewBag.Periodid = new SelectList(db.Periods, "id", "Name");
                    return View();

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,surname,BDate,Certificate,CType,State,Centerid,Periodid,SDate,EDate,Job")] Employee employee)
        {
            try
            {
                employee.id = db.Employees.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                employee.id = 1;
            }
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var typeName = (string)Session["Type"];
            var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
            if (type.Managment == true)
            {

                ViewBag.Centerid = new SelectList(db.Centers, "id", "Name");
                ViewBag.Job = new SelectList(db.EmployeeTypes.Where(x => x.Basics != true && x.Observing != true), "id", "Type");
                ViewBag.Periodid = new SelectList(db.Periods, "id", "Name");
                return View(employee);

            }
            if (type.Basics == true)
            {

                ViewBag.Centerid = new SelectList(db.Centers, "id", "Name");
                ViewBag.Job = new SelectList(db.EmployeeTypes.Where(x => x.Guidence != true && x.Teaching != true), "id", "Type");
                ViewBag.Periodid = new SelectList(db.Periods, "id", "Name");
                return View(employee);

            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {
                    Employee employee = db.Employees.Find(id);
                    if (employee == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Centerid = new SelectList(db.Centers, "id", "Name", employee.Centerid);
                    ViewBag.Job = new SelectList(db.EmployeeTypes, "id", "Type", employee.Job);
                    ViewBag.Periodid = new SelectList(db.Periods, "id", "Name", employee.Periodid);
                    return View(employee);

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,surname,BDate,Certificate,CType,State,Centerid,Periodid,SDate,EDate,Job")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Centerid = new SelectList(db.Centers, "id", "Name", employee.Centerid);
            ViewBag.Job = new SelectList(db.EmployeeTypes, "id", "Type", employee.Job);
            ViewBag.Periodid = new SelectList(db.Periods, "id", "Name", employee.Periodid);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {
                    Employee employee = db.Employees.Find(id);
                    if (employee == null)
                    {
                        return HttpNotFound();
                    }
                    return View(employee);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                var typeName = (string)Session["Type"]; var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {
                    Employee employee = db.Employees.Find(id);
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");

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
