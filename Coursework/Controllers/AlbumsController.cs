using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coursework.Models;

namespace Coursework.Controllers
{
    public class AlbumsController : Controller
    {

        private CourseworkContext db = new CourseworkContext();
        
        // GET: Albums
        public ActionResult Index(string sortOrder)
        {
            if (Session["assis"] != null || Session["manag"] !=null || Session["memb"] !=null)
            {

                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

                var albums = from a in db.Albums select a;
                switch (sortOrder)
                {
                    case "date_desc":
                        albums = albums.OrderByDescending(a => a.ReleasedDate);
                        break;
                    default:
                        albums = albums.OrderBy(a => a.ReleasedDate);
                        break;
                }
                return View(albums.ToList());
            }
            return null;
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Album album = db.Albums.Find(id);
                if (album == null)
                {
                    return HttpNotFound();
                }
                return View(album);
            }
            return null;
        }
        public ActionResult ArtistAlbum(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dvdartists = from a in db.Artists join b in db.AlbumArtists on a.ArtistId equals b.ArtistId join c in db.Albums on b.AlbumId equals c.AlbumId where c.AlbumId == id select a;
            if (dvdartists == null)
            {
                return HttpNotFound();
            }
            return View(dvdartists);
        }
        public ActionResult ProducerAlbum(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dvdproducer = from a in db.Producers join b in db.AlbumProducers on a.ProducerId equals b.ProducerId join c in db.Albums on b.AlbumId equals c.AlbumId where c.AlbumId == id select a;
            if (dvdproducer == null)
            {
                return HttpNotFound();
            }
            return View(dvdproducer);
        }
        // GET: Albums/Create
        public ActionResult Create()
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                ViewBag.AlbumTypeId = new SelectList(db.AlbumTypes, "AlbumTypeId", "AlbumTypeName");
                return View();
            }
            return null;
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album, HttpPostedFileBase file)
        {
            string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
            file.SaveAs(path);
            if (ModelState.IsValid)
            {

                db.Albums.Add(new Album
                {
                    AlbumId = album.AlbumId,
                    Name = album.Name,
                    ReleasedDate = album.ReleasedDate,
                    NoofSongs = album.NoofSongs,
                    TotalLength = album.TotalLength,
                    CopyNumber = album.CopyNumber,
                    StandardCharge = album.StandardCharge,
                    AgeRestricted = album.AgeRestricted,
                    CoverImagePath = "~/Images/" + file.FileName,
                    AlbumTypeId = album.AlbumTypeId,
                    Studio = album.Studio
                });
                db.SaveChanges();
                return RedirectToAction("../artists/create");
            }

            ViewBag.AlbumTypeId = new SelectList(db.AlbumTypes, "AlbumTypeId", "AlbumTypeName", album.AlbumTypeId);
            return View(album);

        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Album album = db.Albums.Find(id);
                if (album == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AlbumTypeId = new SelectList(db.AlbumTypes, "AlbumTypeId", "AlbumTypeName", album.AlbumTypeId);
                return View(album);
            }
            return null;
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Name,ReleasedDate,NoofSongs,TotalLength,CopyNumber,StandardCharge,AgeRestricted,CoverImagePath,AlbumTypeId,Studio")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("../officeuser/album");
            }
            ViewBag.AlbumTypeId = new SelectList(db.AlbumTypes, "AlbumTypeId", "AlbumTypeName", album.AlbumTypeId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Album album = db.Albums.Find(id);
                if (album == null)
                {
                    return HttpNotFound();
                }
                return View(album);
            }
            return null;
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("../officeuser/album");
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
