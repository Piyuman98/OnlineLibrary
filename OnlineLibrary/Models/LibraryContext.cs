using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace OnlineLibrary.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}