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
    public class AlbumTypesController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: AlbumTypes
        public ActionResult Index()
        {
            return View(db.AlbumTypes.ToList());
        }

        // GET: AlbumTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumType albumType = db.AlbumTypes.Find(id);
            if (albumType == null)
            {
                return HttpNotFound();
            }
            return View(albumType);
        }

        // GET: AlbumTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlbumTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumTypeId,AlbumTypeName")] AlbumType albumType)
        {
            if (ModelState.IsValid)
            {
                db.AlbumTypes.Add(albumType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(albumType);
        }

        // GET: AlbumTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumType albumType = db.AlbumTypes.Find(id);
            if (albumType == null)
            {
                return HttpNotFound();
            }
            return View(albumType);
        }

        // POST: AlbumTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumTypeId,AlbumTypeName")] AlbumType albumType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(albumType);
        }

        // GET: AlbumTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumType albumType = db.AlbumTypes.Find(id);
            if (albumType == null)
            {
                return HttpNotFound();
            }
            return View(albumType);
        }

        // POST: AlbumTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumType albumType = db.AlbumTypes.Find(id);
            db.AlbumTypes.Remove(albumType);
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
