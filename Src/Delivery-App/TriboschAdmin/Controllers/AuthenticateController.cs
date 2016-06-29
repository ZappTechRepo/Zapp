
using Homemation.WebAPI.Filters;
using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using TriboschAdmin.Models;

namespace Homemation.WebAPI.Controllers
{
    //[APIAuthenticationFilter]
    [RoutePrefix("api/authenticate")]
    public class AuthenticateController : System.Web.Http.ApiController
    {
        #region Private variable



        #endregion

        #region Public Constructor  

        /// <summary>  
        /// Public constructor to initialize product service instance  
        /// </summary>  
        public AuthenticateController()
        {
            
        }

        #endregion

        /// <summary>  
        /// Authenticates user and returns token with expiry.  
        /// </summary>  
        /// <returns></returns>  
        //[POST("login")]  
        //[POST("authenticate")]  
        //[POST("get/token")] 
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public HttpResponseMessage Authenticate()
        {

            //if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            //{
                // var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                TriboschAdmin.Models.User user = new User();

                if (user.IsValid("jason@live.com", "test123"))
                {
                    return GetAuthToken();
                }

            //}

            return null;
        }

        /// <summary>  
        /// Returns auth token for the validated user.  
        /// </summary>  
        /// <param name="userId"></param>  
        /// <returns></returns>  
        private HttpResponseMessage GetAuthToken()
        {
            //var token = _tokenServices.GenerateToken(userId);
            var deliveryuser = "";
            //var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");  
            var response = Request.CreateResponse(HttpStatusCode.OK, new { UserId = "", Name = "", Surname = "", Phone = "" });
            response.Headers.Add("Token", Guid.NewGuid().ToString());
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");

            return response;
        }
    }
    
}
