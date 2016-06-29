using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace TriboschAdmin.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        protected override ExceptionResult InternalServerError( Exception exception )
        {
            //Utils.Logger.LogException( exception );
            //  Utils.Logger.EmailWebException( filterContext.Exception, "ADF error:", filterContext.HttpContext );

            return base.InternalServerError( exception );
        }
    }
}