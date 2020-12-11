using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coursework.Models;

namespace Coursework.Controllers
{
    public class MemberCategoriesController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: MemberCategories
        public ActionResult Index()
        {
            return View(db.MemberCategories.ToList());
        }

        // GET: MemberCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCategory memberCategory = db.MemberCategories.Find(id);
            if (memberCategory == null)
            {
                return HttpNotFound();
            }
            return View(memberCategory);
        }

        // GET: MemberCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberCategoryId,CategoryName,Description,LoanAvailable,FinePerExtraDays")] MemberCategory memberCategory)
        {
            if (ModelState.IsValid)
            {
                db.MemberCategories.Add(memberCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberCategory);
        }

        // GET: MemberCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCategory memberCategory = db.MemberCategories.Find(id);
            if (memberCategory == null)
            {
                return HttpNotFound();
            }
            return View(memberCategory);
        }

        // POST: MemberCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberCategoryId,CategoryName,Description,LoanAvailable,FinePerExtraDays")] MemberCategory memberCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberCategory);
        }

        // GET: MemberCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCategory memberCategory = db.MemberCategories.Find(id);
            if (memberCategory == null)
            {
                return HttpNotFound();
            }
            return View(memberCategory);
        }

        // POST: MemberCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberCategory memberCategory = db.MemberCategories.Find(id);
            db.MemberCategories.Remove(memberCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
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
