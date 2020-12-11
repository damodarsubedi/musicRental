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
    public class AlbumCopiesController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: AlbumCopies
        public ActionResult Index(string searchString)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {

                var albumCopies = db.AlbumCopies.Include(a => a.Albums);
                var albumc = from b in db.Albums join a in db.AlbumCopies on b.AlbumId equals a.AlbumId where b.Name == searchString select a;
                if (!String.IsNullOrEmpty(searchString))
                {

                    albumCopies = albumc;
                }
                return View(albumCopies.ToList());
            }
            return null;
        }

        // GET: AlbumCopies/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AlbumCopy albumCopy = db.AlbumCopies.Find(id);
                if (albumCopy == null)
                {
                    return HttpNotFound();
                }
                return View(albumCopy);
            }
            return null;
        }

        // GET: AlbumCopies/Create
        public ActionResult Create()
        {
            if (Session["assis"] != null || Session["manag"] != null )
            {

                ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name");
                return View();
            }
            return null;
        }

        // POST: AlbumCopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumCopyId,IssueDate,DvdCopyNumber,AlbumId,CopyStatus")] AlbumCopy albumCopy)
        {
            if (ModelState.IsValid)
            {
                db.AlbumCopies.Add(albumCopy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumCopy.AlbumId);
            return View(albumCopy);
        }

        // GET: AlbumCopies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null )
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AlbumCopy albumCopy = db.AlbumCopies.Find(id);
                if (albumCopy == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumCopy.AlbumId);
                return View(albumCopy);

            }
            return null;
        }

        // POST: AlbumCopies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumCopyId,IssueDate,DvdCopyNumber,AlbumId,CopyStatus")] AlbumCopy albumCopy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumCopy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumCopy.AlbumId);
            return View(albumCopy);
        }

        // GET: AlbumCopies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null )
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AlbumCopy albumCopy = db.AlbumCopies.Find(id);
                if (albumCopy == null)
                {
                    return HttpNotFound();
                }
                return View(albumCopy);
            }
            return null;
        }

        // POST: AlbumCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumCopy albumCopy = db.AlbumCopies.Find(id);
            db.AlbumCopies.Remove(albumCopy);
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
