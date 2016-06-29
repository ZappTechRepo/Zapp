using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using TriboschAdmin.Models;
using TriboschAdmin.WebAPI.Filters;

namespace TriboschAdmin.WebAPI.Controllers
{
    [APIAuthenticationFilter]
    [RoutePrefix("api/authenticate")]
    public class AuthenticateController : BaseController
    {
        #region Private variable

        private readonly ITokenServices _tokenServices;



        #endregion

        #region Public Constructor  

        /// <summary>  
        /// Public constructor to initialize product service instance  
        /// </summary>  
        public AuthenticateController(ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
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
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Authenticate()
        {

            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;

                    return GetAuthToken(userId);
                }
            }



            return null;
        }

        /// <summary>  
        /// Returns auth token for the validated user.  
        /// </summary>  
        /// <param name="userId"></param>  
        /// <returns></returns>  
        private HttpResponseMessage GetAuthToken( int userid)
    {
            var token = _tokenServices.GenerateToken(userid);
            var deliveryuser = _tokenServices.ProfileDetail(userid);
            //var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");  
            var response = Request.CreateResponse(HttpStatusCode.OK, new { UserName = deliveryuser.UserID, Role = deliveryuser.FullName, Email = deliveryuser.FullName});
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");

            return response;
        }
}  


    
}
