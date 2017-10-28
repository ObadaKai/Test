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
    public class StudentsController : Controller
    {
        private TaalimEntities db = new TaalimEntities();

        // GET: Students
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true)
                {

                    var students = db.Students.Include(s => s.Center).Include(s => s.Regiment).Include(s => s.Stage);
                    return View(students.ToList());



                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true)
                {

                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);



                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Students/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {

                    ViewBag.Centerid = new SelectList(db.Centers, "id", "Name");
                    ViewBag.Regimentid = new SelectList(db.Regiments, "id", "Name");
                    ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName");
                    return View();



                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(/*[Bind(Include = "BDate,Name,Surname,Certificate,Mark,State,Centerid,Stageid,Regimentid,SDate,EDate")]*/ Student student)
        {
            try
            {
                student.id = db.Students.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                student.id = 1;
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return Json(true);
            }

            ViewBag.Centerid = new SelectList(db.Centers, "id", "Name", student.Centerid);
            ViewBag.Regimentid = new SelectList(db.Regiments, "id", "Name", student.Regimentid);
            ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", student.Stageid);
            return Json(false);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {

                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Centerid = new SelectList(db.Centers, "id", "Name", student.Centerid);
                    ViewBag.Regimentid = new SelectList(db.Regiments, "id", "Name", student.Regimentid);
                    ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", student.Stageid);
                    return View(student);



                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,BDate,Name,Surname,Certificate,Mark,State,Centerid,Stageid,Regimentid,SDate,EDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Centerid = new SelectList(db.Centers, "id", "Name", student.Centerid);
            ViewBag.Regimentid = new SelectList(db.Regiments, "id", "Name", student.Regimentid);
            ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", student.Stageid);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {

                    Student student = db.Students.Find(id);
                    if (student == null)
                    {
                        return HttpNotFound();
                    }
                    return View(student);



                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {

                    Student student = db.Students.Find(id);
                    db.Students.Remove(student);
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
