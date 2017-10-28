﻿using System;
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
    public class StagesController : Controller
    {
        private TaalimEntities db = new TaalimEntities();

        // GET: Stages
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Basics == true)
                {
                    var stages = db.Stages.Include(s => s.Employee);
                    return View(stages.ToList());
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Stages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Basics == true)
                {
                    Stage stage = db.Stages.Find(id);
                    if (stage == null)
                    {
                        return HttpNotFound();
                    }
                    return View(stage);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Stages/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Basics == true)
                {
                    ViewBag.Managerid = new SelectList(db.Employees, "id", "name");
                    return View();
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Stages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StageName,Managerid,SDate,EDate")] Stage stage)
        {
            try
            {
                stage.id = db.Stages.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                stage.id = 1;
            }
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Managerid = new SelectList(db.Employees, "id", "name", stage.Managerid);
            return View(stage);
        }

        // GET: Stages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Basics == true)
                {
                    Stage stage = db.Stages.Find(id);
                    if (stage == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Managerid = new SelectList(db.Employees, "id", "name", stage.Managerid);
                    return View(stage);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,StageName,Managerid,SDate,EDate")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Managerid = new SelectList(db.Employees, "id", "name", stage.Managerid);
            return View(stage);
        }

        // GET: Stages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Basics == true)
                {
                    Stage stage = db.Stages.Find(id);
                    if (stage == null)
                    {
                        return HttpNotFound();
                    }
                    return View(stage);
                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Basics == true)
                {
                    Stage stage = db.Stages.Find(id);
                    db.Stages.Remove(stage);
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
