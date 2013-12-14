using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFEWeb.Models
{
    public class ContactMailModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string NewsLetter { get; set; }
    }
}