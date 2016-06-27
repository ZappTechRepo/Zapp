
using Homemation.WebAPI.Filters;
using Homemation.WebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TriboschAdmin;
using TriboschAdmin.Models;

namespace Homemation.WebAPI.Controllers
{
    [APIAuthenticationFilter]
    [System.Web.Http.RoutePrefix("api/authenticate")]
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
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public HttpResponseMessage Authenticate()
        {

            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                // var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                TriboschAdmin.Models.User user = new TriboschAdmin.Models.User();

                if (user.IsValid(user.UserName, user.Password))
                {
                    return GetAuthToken();
                }

            }

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


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Amrod.App.Api.Code.Response.Auth</returns>
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage doLogin([FromBody]Login input)
        {
            AuthResults response = new AuthResults() { Success = false };

            if (input == null)
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new ArgumentException("", "input"));
            if (string.IsNullOrEmpty(input.userName))
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new ArgumentException("", "userName"));
            if (string.IsNullOrEmpty(input.password))
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, new ArgumentException("", "password"));

            try
            {
                TriboschAppEntities db = new TriboschAppEntities();
                TriboschAdmin.User _user = db.Users.FirstOrDefault(q => q.Username.Equals(input.userName));
                if (_user != null)
                {
                    if (_user.Password == input.password)
                    {
                        //Auth _auth = db.Auth.FirstOrDefault(x => x.UserID == _user.ID);
                        //if (_auth == null)
                        //{
                        //    _auth = new Auth();
                        //    _auth.UserID = _user.ID;
                        //    _auth.AuthKey = RandomString(64);
                        //    _auth.Created = DateTime.Now;

                        //    db.Entry(_auth).State = EntityState.Added;
                        //    db.SaveChanges();
                        //}

                        //response.User = _user;
                        response.Token = Guid.NewGuid().ToString();
                        response.Success = true;
                    }
                    else
                    {
                        response.Success = false;
                        response.AuthResponse = "Invalid username and password combination!";
                    }
                }
                else
                {
                    response.Success = false;
                    response.AuthResponse = "It seems you are not a member yet, please do the honors and register";
                }

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                //Logger.Log("Login Exception:" + ex.Message);
                //do logging
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
