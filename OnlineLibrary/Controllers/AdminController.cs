using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class AdminController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Admin/SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            var admin = db.Admins.SingleOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                // Redirect to admin dashboard or any other admin functionality
                return RedirectToAction("Dashboard");
            }
            ViewBag.Message = "Invalid credentials";
            return View();
        }

        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Admin/Books
        public ActionResult Books()
        {
            var books = db.Books.ToList();
            return View(books);
        }

        // GET: Admin/CreateBook
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Books");
            }
            return View(book);
        }

        // GET: Admin/EditBook/5
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Books");
            }
            return View(book);
        }

        // GET: Admin/DeleteBook/5
        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Books");
        }

        // GET: Admin/IssueBook
        public ActionResult IssueBook()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IssueBook(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                var book = db.Books.Find(reservation.BookID);
                if (book != null && book.AvailableCopies > 0)
                {
                    book.AvailableCopies--;
                    db.SaveChanges();
                    return RedirectToAction("Books");
                }
                ModelState.AddModelError("", "Not enough copies available.");
            }

            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", reservation.BookID);
            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name", reservation.GuestID);
            return View(reservation);
        }

        // GET: Admin/ReturnBook
        public ActionResult ReturnBook()
        {
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnBook(int bookID, int guestID)
        {
            var reservation = db.Reservations.FirstOrDefault(r => r.BookID == bookID && r.GuestID == guestID);
            if (reservation != null)
            {
                db.Reservations.Remove(reservation);
                var book = db.Books.Find(bookID);
                if (book != null)
                {
                    book.AvailableCopies++;
                    db.SaveChanges();
                }
                return RedirectToAction("Books");
            }

            ModelState.AddModelError("", "No matching reservation found.");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", bookID);
            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name", guestID);
            return View();
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
