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

        public string usn;


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
                    usn = basicAuthenticationIdentity.UserName;
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
        private HttpResponseMessage GetAuthToken(int userid)
        {
            var token = _tokenServices.GenerateToken(userid);
            var deliveryuser = _tokenServices.ProfileDetail(usn);
            bool _success = deliveryuser != null;
            //var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");  
            var response = Request.CreateResponse(HttpStatusCode.OK, new {Success = _success, Token = token.AuthToken, Fullname = deliveryuser.FullName, UserName = deliveryuser.Username, Email = deliveryuser.EmailID, UserID = deliveryuser.UserID });
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");

            return response;
        }


        /// <summary>
        /// doLoginFromToken with previous returned doLogin Auth Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Amrod.App.Api.Code.Response.Auth</returns>
        [AcceptVerbs("POST")]
        [HttpPost]
        public HttpResponseMessage LoginFromToken()
        {
            try
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    int userId = _tokenServices.ValidateToken(basicAuthenticationIdentity.Token);


                    var deliveryuser = _tokenServices.ProfileDetail(userId);
                    bool _success = deliveryuser != null;

                    return GetAuthToken(userId);

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { Success = _success, Token = basicAuthenticationIdentity.Token, Fullname = deliveryuser.FullName, UserName = deliveryuser.Username, Email = deliveryuser.EmailID, UserID = deliveryuser.UserID });

                    response.Headers.Add("Token", basicAuthenticationIdentity.Token);
                    response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
                    response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");

                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, new Exception("Error while authenticating"));
                }

                
            }
            catch (Exception ex)
            {
                //Logger.Log("Token Recall Exception:" + ex.Message);
                //do logging
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
