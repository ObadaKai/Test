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
    public class ExaminationsController : Controller
    {
        private TaalimEntities db = new TaalimEntities();

        // GET: Examinations
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true || type.Guidence == true || type.Teaching == true)
                {
                    var examinations = db.Examinations.Include(e => e.Stage).Include(e => e.Student).Include(e => e.Study_subject).Include(e => e.ExamType);
                    return View(examinations.ToList());
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Examinations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true || type.Guidence == true || type.Teaching == true)
                {
                    Examination examination = db.Examinations.Find(id);
                    if (examination == null)
                    {
                        return HttpNotFound();
                    }
                    return View(examination);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Examinations/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Teaching == true)
                {
                    ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName");
                    ViewBag.Studentid = new SelectList(db.Students, "id", "Name");
                    ViewBag.Subjectid = new SelectList(db.Study_subject, "id", "Name");
                    ViewBag.ExamTypeid = new SelectList(db.ExamTypes, "id", "Type");
                    return View();
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Examinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(/*[Bind(Include = "Desc,Subjectid,Studentid,Stageid,ExamTypeid,Mark,Date")] */Examination examination)
        {
            try
            {
                examination.id = db.Examinations.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                examination.id = 1;
            }

            if (ModelState.IsValid)
            {
                db.Examinations.Add(examination);
                db.SaveChanges();
                return Json(true);
            }

            //ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", examination.Stageid);
            //ViewBag.Studentid = new SelectList(db.Students, "id", "Name", examination.Studentid);
            //ViewBag.Subjectid = new SelectList(db.Study_subject, "id", "Name", examination.Subjectid);
            //ViewBag.ExamTypeid = new SelectList(db.ExamTypes, "id", "Type", examination.ExamTypeid);
            return Json(false);
        }

        // GET: Examinations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Teaching == true)
                {

                    Examination examination = db.Examinations.Find(id);
                    if (examination == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", examination.Stageid);
                    ViewBag.Studentid = new SelectList(db.Students, "id", "Name", examination.Studentid);
                    ViewBag.Subjectid = new SelectList(db.Study_subject, "id", "Name", examination.Subjectid);
                    ViewBag.ExamTypeid = new SelectList(db.ExamTypes, "id", "Type", examination.ExamTypeid);
                    return View(examination);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Desc,Subjectid,Studentid,Stageid,ExamTypeid,Mark,Date")] Examination examination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Stageid = new SelectList(db.Stages, "id", "StageName", examination.Stageid);
            ViewBag.Studentid = new SelectList(db.Students, "id", "Name", examination.Studentid);
            ViewBag.Subjectid = new SelectList(db.Study_subject, "id", "Name", examination.Subjectid);
            ViewBag.ExamTypeid = new SelectList(db.ExamTypes, "id", "Type", examination.ExamTypeid);
            return View(examination);
        }

        // GET: Examinations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Teaching == true)
                {
                    Examination examination = db.Examinations.Find(id);
                    if (examination == null)
                    {
                        return HttpNotFound();
                    }
                    return View(examination);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Teaching == true)
                {
                    Examination examination = db.Examinations.Find(id);
                    db.Examinations.Remove(examination);
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
