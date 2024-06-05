using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int BookID { get; set; }

        public int GuestID { get; set; }

        public DateTime ReservationDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Guest Guest { get; set; }
    }
}