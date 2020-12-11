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
    public class LoansController : Controller
    {
        private CourseworkContext db = new CourseworkContext();

        public int MemberID { get; private set; }

        // GET: Loans
        public ActionResult Index(string searchString, string memberLoan)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                var loans = db.Loans.Include(l => l.AlbumCopies).Include(l => l.Members);
                DateTime startTime = DateTime.Now;
                DateTime date = DateTime.Today.AddDays(-31);
                var copyloan = from a in db.AlbumCopies
                               join b in db.Loans on a.AlbumCopyId 
                               equals b.AlbumCopyId
                               where a.DvdCopyNumber == searchString
                               select b;
                var loanmem = from a in db.Members join b in db.Loans on a.MemberID equals b.MemberId where a.Email == memberLoan && (b.IssuedDate >= date)  select b;
                if (!String.IsNullOrEmpty(searchString))
                {

                    loans = copyloan;
                }
                if (!String.IsNullOrEmpty(memberLoan))
                {
                    loans = loanmem;
                }
                return View(loans.ToList());
            }
            return null;
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Loan loan = db.Loans.Find(id);
                if (loan == null)
                {
                    return HttpNotFound();
                }
                return View(loan);

            }
            return null;
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                ViewBag.AlbumCopyId = new SelectList(db.AlbumCopies, "AlbumCopyId", "DvdCopyNumber");
                ViewBag.MemberId = new SelectList(db.Members, "MemberID", "FirstName");
                return View();
            }
            return null;
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanId,MemberId,AlbumCopyId,IssuedDate,DueDate,ReturnedDate,TotalCharge,LoanTypes")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                
                AlbumCopy albumCopy = db.AlbumCopies.Find(loan.AlbumCopyId);
                DateTime startTime = DateTime.Now;
                
                Member member = db.Members.Single(m => m.MemberID == loan.MemberId);
                var age = startTime.Year - member.Dob.Year;

                int copyLoancount = (from l in db.Loans.ToList()
                              where l.AlbumCopyId == albumCopy.AlbumCopyId
                              select albumCopy
                             ).ToList().Count();

                int loanCount = (from l in db.Loans.ToList()
                                 where l.MemberId == member.MemberID
                                 where l.ReturnedDate == null
                                 select l).ToList().Count();
                //MemberCategory silver = db.MemberCategories.Single(m => m.CategoryName == "Silver");
                //MemberCategory gold = db.MemberCategories.Single(m => m.CategoryName == "Gold");
                //MemberCategory diamond = db.MemberCategories.Single(m => m.CategoryName == "Diamond");
                MemberCategory cata = db.MemberCategories.Single(m => m.MemberCategoryId == member.MemberCategoryId);
                var albumres = (
                    from l in db.Loans
                    join c in db.AlbumCopies
                    on l.AlbumCopyId equals c.AlbumCopyId
                    join a in db.Albums on c.AlbumId equals a.AlbumId
                    select a
                    );
                Album album = db.Albums.Find(albumCopy.AlbumId);
                if (member.MemberCategoryId == cata.MemberCategoryId && loanCount==cata.LoanAvailable && copyLoancount > 1)
                {
                    TempData["Error"] = "loan not available";
                }
                else if(album.AgeRestricted == true && age < 18)
                {
                    TempData["Error"] = "User is small";
                }
                else
                {
                    db.Loans.Add(loan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.AlbumCopyId = new SelectList(db.AlbumCopies, "AlbumCopyId", "DvdCopyNumber", loan.AlbumCopyId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberID", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Loan loan = db.Loans.Find(id);
                if (loan == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AlbumCopyId = new SelectList(db.AlbumCopies, "AlbumCopyId", "DvdCopyNumber", loan.AlbumCopyId);
                ViewBag.MemberId = new SelectList(db.Members, "MemberID", "FirstName", loan.MemberId);
                return View(loan);
            }
            return null;
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanId,MemberId,AlbumCopyId,IssuedDate,DueDate,ReturnedDate,TotalCharge,LoanTypes")] Loan loan)
        {
            if (ModelState.IsValid)
            {

                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumCopyId = new SelectList(db.AlbumCopies, "AlbumCopyId", "DvdCopyNumber", loan.AlbumCopyId);
            ViewBag.MemberId = new SelectList(db.Members, "MemberID", "FirstName", loan.MemberId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["assis"] != null || Session["manag"] != null)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Loan loan = db.Loans.Find(id);
                if (loan == null)
                {
                    return HttpNotFound();
                }
                return View(loan);
            }
            return null;
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
