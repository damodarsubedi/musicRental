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
    public class UsersController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        // GET: Users
        public ActionResult Index()
        {
            if (Session["manag"] != null) {

                var users = db.Users.Include(u => u.UserRoles);
                return View(users.ToList());
            }
            return null;
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["manag"] != null) {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);

            }return null;
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["manag"] != null)
            {
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "UserRoleName");
            return View();
                
            }
            return null;
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,Address,Email,UserContact,Password,UserRoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "UserRoleName", user.UserRoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "UserRoleName", user.UserRoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,Address,Email,UserContact,Password,UserRoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (Session["assis"] != null)
                {
                    return RedirectToAction("../Officeuser/Album");
                }
                return RedirectToAction("Index");
            }
            ViewBag.UserRoleId = new SelectList(db.UserRoles, "UserRoleId", "UserRoleName", user.UserRoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["manag"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);

            }
            return null;
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
