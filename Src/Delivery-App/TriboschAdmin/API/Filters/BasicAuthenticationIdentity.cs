using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace TriboschAdmin.WebAPI.Filters
{
    /// <summary>  
    /// Basic Authentication identity  
    /// </summary>  
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        /// <summary>  
        /// Get/Set for word  
        /// </summary>  
        public string word
        {
            get;
            set;
        }
        /// <summary>  
        /// Get/Set for UserName  
        /// </summary>  
        public string UserName
        {
            get;
            set;
        }
        /// <summary>  
        /// Get/Set for UserId  
        /// </summary>  
        public int UserId
        {
            get;
            set;
        }

        public string Token
        {
            get;
            set;
        }

        /// <summary>  
        /// Basic Authentication Identity Constructor  
        /// </summary>  
        /// <param name="userName"></param>  
        /// <param name="word"></param>  
        public BasicAuthenticationIdentity(string userName, string pword, string token)
            : base(userName, "Basic")
        {
            word = pword;
            UserName = userName;
            Token = token;
        }
    }  
}