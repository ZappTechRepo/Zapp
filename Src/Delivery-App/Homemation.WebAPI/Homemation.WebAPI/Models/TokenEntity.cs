using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homemation.WebAPI.Models
{
    public class TokenEntity
    {
         public Guid UserId {get; set;}
         public DateTime IssuedOn {get; set;}
         public DateTime ExpiresOn {get; set;}
         public String AuthToken {get; set;}
            
    }
}