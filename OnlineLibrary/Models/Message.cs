using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
    }
}