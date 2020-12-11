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
    public class ProducersController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: Producers
        public ActionResult Index()
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {

                return View(db.Producers.ToList());
            }
            return null;
        }

        // GET: Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producer producer = db.Producers.Find(id);
                if (producer == null)
                {
                    return HttpNotFound();
                }
                return View(producer);
            }
            return null;
        }

        // GET: Producers/Create
        public ActionResult Create()
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {
                return View();

            }
            return null;
        }

        // POST: Producers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducerId,ProducerName,ProducerAddress,ProducerPhone")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Producers.Add(producer);
                db.SaveChanges();
                return RedirectToAction("../Albumproducers/create");
            }

            return View(producer);
        }

        // GET: Producers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producer producer = db.Producers.Find(id);
                if (producer == null)
                {
                    return HttpNotFound();
                }
                return View(producer);
            }
            return null;
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProducerId,ProducerName,ProducerAddress,ProducerPhone")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producer producer = db.Producers.Find(id);
                if (producer == null)
                {
                    return HttpNotFound();
                }
                return View(producer);
            }
            return null;
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producer producer = db.Producers.Find(id);
            db.Producers.Remove(producer);
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
