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
    public class RegimentsController : Controller
    {
        private TaalimEntities db = new TaalimEntities();

        // GET: Regiments
        public ActionResult Index()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Observing == true || type.Managment == true)
                {




                    var regiments = db.Regiments.Include(r => r.Period);
                    return View(regiments.ToList());

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Regiments/Details/5
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




                    Regiment regiment = db.Regiments.Find(id);
                    if (regiment == null)
                    {
                        return HttpNotFound();
                    }
                    return View(regiment);

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // GET: Regiments/Create
        public ActionResult Create()
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {




                    ViewBag.Periodid = new SelectList(db.Periods, "id", "Name");
                    return View();

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Regiments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Day1,Day2,Day3,Day4,Day5,Day6,Day7,Desc,Periodid")] Regiment regiment)
        {
            try
            {
                regiment.id = db.Regiments.OrderByDescending(x => x.id).FirstOrDefault().id + 1;
            }
            catch (Exception)
            {
                regiment.id = 1;
            }
            if (ModelState.IsValid)
            {
                db.Regiments.Add(regiment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Periodid = new SelectList(db.Periods, "id", "Name", regiment.Periodid);
            return View(regiment);
        }

        // GET: Regiments/Edit/5
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




                    Regiment regiment = db.Regiments.Find(id);
                    if (regiment == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.Periodid = new SelectList(db.Periods, "id", "Name", regiment.Periodid);
                    return View(regiment);

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Regiments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Day1,Day2,Day3,Day4,Day5,Day6,Day7,Desc,Periodid")] Regiment regiment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regiment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Periodid = new SelectList(db.Periods, "id", "Name", regiment.Periodid);
            return View(regiment);
        }

        // GET: Regiments/Delete/5
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




                    Regiment regiment = db.Regiments.Find(id);
                    if (regiment == null)
                    {
                        return HttpNotFound();
                    }
                    return View(regiment);

                }
                return RedirectToAction("Default", "Home");
            }
            return RedirectToAction("Index", "Home");


        }

        // POST: Regiments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID"] != null)
            {
                                var typeName = (string)Session["Type"];var type = db.EmployeeTypes.Where(x => x.Type == typeName).FirstOrDefault();
                if (type.Managment == true)
                {




                    Regiment regiment = db.Regiments.Find(id);
                    db.Regiments.Remove(regiment);
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
