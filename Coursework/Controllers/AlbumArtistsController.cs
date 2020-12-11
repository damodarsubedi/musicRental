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
    public class AlbumArtistsController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: AlbumArtists
        public ActionResult Index()
        {
            var albumArtists = db.AlbumArtists.Include(a => a.Albums).Include(a => a.Artists);
            return View(albumArtists.ToList());
        }

        // GET: AlbumArtists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumArtist albumArtist = db.AlbumArtists.Find(id);
            if (albumArtist == null)
            {
                return HttpNotFound();
            }
            return View(albumArtist);
        }

        // GET: AlbumArtists/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "FirstName");
            return View();
        }

        // POST: AlbumArtists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumArtistId,ArtistId,AlbumId")] AlbumArtist albumArtist)
        {
            if (ModelState.IsValid)
            {
                db.AlbumArtists.Add(albumArtist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumArtist.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "FirstName", albumArtist.ArtistId);
            return View(albumArtist);
        }

        // GET: AlbumArtists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumArtist albumArtist = db.AlbumArtists.Find(id);
            if (albumArtist == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumArtist.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "FirstName", albumArtist.ArtistId);
            return View(albumArtist);
        }

        // POST: AlbumArtists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumArtistId,ArtistId,AlbumId")] AlbumArtist albumArtist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumArtist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "AlbumId", "Name", albumArtist.AlbumId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "FirstName", albumArtist.ArtistId);
            return View(albumArtist);
        }

        // GET: AlbumArtists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumArtist albumArtist = db.AlbumArtists.Find(id);
            if (albumArtist == null)
            {
                return HttpNotFound();
            }
            return View(albumArtist);
        }

        // POST: AlbumArtists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumArtist albumArtist = db.AlbumArtists.Find(id);
            db.AlbumArtists.Remove(albumArtist);
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
