using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace Homemation.WebAPI.Filters
{
    
    /// <summary>  
    /// Custom Authentication Filter Extending basic Authentication  
    /// </summary>  
    public class APIAuthenticationFilter : GenericAuthenticationFilter
    {
        /// <summary>  
        /// Default Authentication Constructor  
        /// </summary>  
        public APIAuthenticationFilter() { }

        /// <summary>  
        /// AuthenticationFilter constructor with isActive parameter  
        /// </summary>  
        /// <param name="isActive"></param>  
        public APIAuthenticationFilter(bool isActive) : base(isActive) { }  
  


        /// <summary>
        /// Protected overriden method for authorizing user  
        /// overrides the OnAuthorizeUser method to add custom logic for authenticating a request
        /// It uses UserService that we created earlier to check the user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="word"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool OnAuthorizeUser(string username, string word, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(IUserService)) as IUserService;
            if (provider != null)
            {
               
                var userId = provider.Authenticate(username, word);
               
                //if (userId > 0)
                if (userId != Guid.Empty)
                {

                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null) basicAuthenticationIdentity.UserId = userId;
                    return true;
                }
            }
            return false;
        } 
 

    }
}