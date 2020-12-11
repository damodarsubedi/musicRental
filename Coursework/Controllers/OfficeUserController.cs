using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    public class OfficeUserController : Controller
    {
        private CourseworkContext db = new CourseworkContext();
        // GET: OfficeUser
        public ActionResult Login()
        {
            return View(db.UserRoles.ToList());
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password, int UserRoleId)
        {
            var em = (from u in db.Users where u.Email == Email where u.Password == Password where u.UserRoleId == UserRoleId select u).ToList();
            if (em.Count() == 0)
            {
                TempData["LoginError"] = "Username and password not valid";
                return View(db.UserRoles.ToList());
            }
            
            //string i= (from u in db.Users join r in db.UserRoles on u.UserRoleId equals r.UserRoleId where em[0].UserRoleId == u.UserRoleId select r.UserRoleName).ToString();
            if (UserRoleId == 1 )
            {
                Session["assis"] = em[0].UserId;
                Session["a"] = "a";
                return Redirect("/Officeuser/album");
            }
            else if(UserRoleId == 2)
            {
                Session["manag"] = em[0].UserId;
                Session["Id"] = "a";
                return Redirect("/Officeuser/album");
            }
            return View(db.UserRoles.ToList());
        }


        public ActionResult SignUp()
        {
            return View(db.UserRoles.ToList());
        }


        [HttpPost]
        public ActionResult SignUp(string FirstName,string LastName, string Address, string Email, string UserContact, string Password, int  UserRoleId)
        {
            var em = (from u in db.Users where u.Email == Email select u).ToList();
            if (em.Count() > 0)
            {
                TempData["SignupError"] = "Email is already taken";
                return View(db.UserRoles.ToList());
            }
            User user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address=Address,
                Email = Email,
                UserContact = UserContact,
                Password = Password,
                UserRoleId = UserRoleId
            };
            db.Users.Add(user);
            db.SaveChanges();
            return View(db.UserRoles.ToList());
        }

        
        public ActionResult Album(string sortOrder, string searchString)
        {
            if (Session["manag"]!=null || Session["assis"]!=null || Session["memb"]!=null)
            {
                var albums = from a in db.Albums select a;
                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                var dvdartists = from a in db.Artists join b in db.AlbumArtists on a.ArtistId equals b.ArtistId join c in db.Albums on b.AlbumId equals c.AlbumId where a.LastName == searchString select c;
                if (!String.IsNullOrEmpty(searchString))
                {

                    albums = dvdartists;
                }

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
        public Action Logout(int id)
        {
            if (id != null)
            {

            }
            return null;
        }
      

    }
}