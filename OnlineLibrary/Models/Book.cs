using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Book
    {
        public int BookID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [StringLength(100)]
        public string Genre { get; set; }

        public int PublishedYear { get; set; }

        public int AvailableCopies { get; set; }
    }
}