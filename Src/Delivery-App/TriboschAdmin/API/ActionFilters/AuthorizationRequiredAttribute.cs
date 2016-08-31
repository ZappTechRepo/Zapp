using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Homemation.WebAPI.ActionFilters
{

    /// <summary>
    ///  this the way we define an action filter in an API project.
    ///  [AuthorizationRequired] We have marked our controller with the action filter that we created, now every request coming to the actions of this controller will need to be ed using this ActionFilter, that checks for the token in the request.
    /// </summary>
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";


        /// <summary>
        /// checks for the “Token” attribute in the Header of every request. If the token is present, it calls the 
        /// ValidateToken method from TokenServices to check if the token exists in the database. 
        /// If the token is valid, our request is navigated to the actual controller and the action that we 
        /// requested, else you'll get an error message saying unauthorized.
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            // Get API key provider  
            var provider = filterContext.ControllerContext.Configuration.DependencyResolver.GetService(typeof(ITokenServices)) as ITokenServices;

            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();

                // Validate Token  
                if (provider != null && provider.ValidateToken(tokenValue) != -1)
                {
                    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                    {
                        ReasonPhrase = "Invalid Request"
                    };
                    filterContext.Response = responseMessage;
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }   
}