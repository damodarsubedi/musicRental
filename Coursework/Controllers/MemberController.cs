using Coursework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework.Controllers
{
    
    public class MemberController : Controller
    {
        private CourseworkContext db = new CourseworkContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var em = (from m in db.Members where m.Email == Email where m.Password==Password select m).ToList();
            if (em.Count() == 0)
            {
                TempData["LoginError"] = "Username and password not valid";
                return View();
            }
            Session["memb"] = em[0].MemberID;
            return Redirect("/Officeuser/album");
        }
        public ActionResult SignUp()
        {

            return View(db.MemberCategories.ToList());
        }
        [HttpPost]
        public ActionResult SignUp(string FirstName, string LastName, string Email, string Password, DateTime Dob, string PhoneNo, int MemberCategory)
        {
            var em = (from m in db.Members where m.Email == Email select m).ToList();
            if (em.Count() > 0)
            {
                TempData["SignupError"] = "Email is already taken";
                return View(db.MemberCategories.ToList());
            }
            Member member = new Member() {
                FirstName= FirstName,
                LastName= LastName,
                Email= Email,
                Password= Password,
                Dob= Dob,
                MemberCategoryId=MemberCategory
            };
            db.Members.Add(member);
            db.SaveChanges();
            return View(db.MemberCategories.ToList());
        }
    }
}