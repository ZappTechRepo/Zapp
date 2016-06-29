using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriboschAdmin.Models
{
    public class TokenEntity
    {
        public int UserId { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public String AuthToken { get; set; }
    }
}