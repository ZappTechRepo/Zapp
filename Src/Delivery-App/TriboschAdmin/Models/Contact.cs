using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.Models
{
    public class Contact
    {
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }

        public List<UserAddress> Addresses { get; set; }
    }
}