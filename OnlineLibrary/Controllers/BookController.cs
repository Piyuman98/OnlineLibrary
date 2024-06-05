using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineLibrary.Models;

namespace OnlineLibrary.Controllers
{
    public class BookController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Book
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Additional CRUD actions (Edit, Delete, Details) go here
    }
}