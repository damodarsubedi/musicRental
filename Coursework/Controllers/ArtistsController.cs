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
    public class ArtistsController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: Artists
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                var artists = from a in db.Artists select a;
                if (!String.IsNullOrEmpty(searchString))
                {
                    artists = artists.Where(a => a.LastName.Contains(searchString) || a.FirstName.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        artists = artists.OrderByDescending(a => a.LastName);
                        break;
                    default:
                        artists = artists.OrderBy(s => s.LastName);
                        break;
                }
                return View(artists.ToList());
            }
            return null;
             
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null || Session["memb"] != null)
            {


                    if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Artist artist = db.Artists.Find(id);
                if (artist == null)
                {
                    return HttpNotFound();
                }
                return View(artist);
            }
            return null;
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                return View();
            }
            return null;
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,FirstName,LastName,Gender,Email,ContactNumber,BirthDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("../Producers/Create");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Artist artist = db.Artists.Find(id);
                if (artist == null)
                {
                    return HttpNotFound();
                }
                return View(artist);
            }
            return null;
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistId,FirstName,LastName,Gender,Email,ContactNumber,BirthDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Artist artist = db.Artists.Find(id);
                if (artist == null)
                {
                    return HttpNotFound();
                }
                return View(artist);
            }
            return null;
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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
