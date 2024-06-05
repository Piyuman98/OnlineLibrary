using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class GuestController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Guest/Search
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            var books = db.Books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString) || b.Genre.Contains(searchString)).ToList();
            return View("SearchResults", books);
        }

        // GET: Guest/ReserveBook
        public ActionResult ReserveBook(int? id)
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
            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name");
            return View(new Reservation { BookID = book.BookID, ReservationDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveBook(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                var book = db.Books.Find(reservation.BookID);
                if (book != null && book.AvailableCopies > 0)
                {
                    book.AvailableCopies--;
                    db.SaveChanges();
                    return RedirectToAction("Search");
                }
                ModelState.AddModelError("", "Not enough copies available.");
            }

            ViewBag.GuestID = new SelectList(db.Guests, "GuestID", "Name", reservation.GuestID);
            return View(reservation);
        }

        // GET: Guest/LeaveMessage
        public ActionResult LeaveMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Search");
            }
            return View(message);
        }
        // GET: Guest/MessageConfirmation
        public ActionResult MessageConfirmation()
        {
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
