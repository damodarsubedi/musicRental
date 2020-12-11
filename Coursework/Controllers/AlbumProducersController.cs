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
    public class AlbumProducersController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: AlbumProducers
        public ActionResult Index()
        {
            var albumProducers = db.AlbumProducers.Include(a => a.Albums).Include(a => a.Producers);
            return View(albumProducers.ToList());
        }

        // GET: AlbumProducers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumProducer albumProducer = db.AlbumProducers.Find(id);
            if (albumProducer == null)
            {
                return HttpNotFound();
            }
            return View(albumProducer);
        }

        // GET: AlbumProducers/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name");
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName");
            return View();
        }

        // POST: AlbumProducers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumProducerId,AlbumId,ProducerId")] AlbumProducer albumProducer)
        {
            if (ModelState.IsValid)
            {
                db.AlbumProducers.Add(albumProducer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumProducer.AlbumId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", albumProducer.ProducerId);
            return View(albumProducer);
        }

        // GET: AlbumProducers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumProducer albumProducer = db.AlbumProducers.Find(id);
            if (albumProducer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumProducer.AlbumId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", albumProducer.ProducerId);
            return View(albumProducer);
        }

        // POST: AlbumProducers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumProducerId,AlbumId,ProducerId")] AlbumProducer albumProducer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumProducer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumProducer.AlbumId);
            ViewBag.ProducerId = new SelectList(db.Producers, "ProducerId", "ProducerName", albumProducer.ProducerId);
            return View(albumProducer);
        }

        // GET: AlbumProducers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumProducer albumProducer = db.AlbumProducers.Find(id);
            if (albumProducer == null)
            {
                return HttpNotFound();
            }
            return View(albumProducer);
        }

        // POST: AlbumProducers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumProducer albumProducer = db.AlbumProducers.Find(id);
            db.AlbumProducers.Remove(albumProducer);
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
