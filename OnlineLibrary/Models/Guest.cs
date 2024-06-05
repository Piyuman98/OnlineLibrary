using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Guest
    {
        public int GuestID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}