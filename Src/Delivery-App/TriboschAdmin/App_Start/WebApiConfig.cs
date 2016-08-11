using Homemation.WebAPI.Models;
using Homemation.WebAPI.Repository;
using Microsoft.Practices.Unity;
using ProductStore.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using TriboschAdmin.API.Models;
using TriboschAdmin.API.Repositories;
using TriboschAdmin.WebAPI.Repository;

namespace TriboschAdmin.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { category = "all", id = RouteParameter.Optional }
            );

            //configuration.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "api/{controller}/{action}/{id}",
            //     defaults: new { category = "all", id = RouteParameter.Optional });

            //configuration.Routes.MapHttpRoute(
            //      name: "DefaultApiDocs",
            //      routeTemplate: "api/{controller}/{category}",
            //      defaults: new { category = "all" }
            //    );

            configuration.Routes.MapHttpRoute("DefaultApiWithId", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            configuration.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}");
            configuration.Routes.MapHttpRoute("DefaultApiGet", "api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            configuration.Routes.MapHttpRoute("DefaultApiPost", "api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            configuration.Routes.MapHttpRoute("DefaultApiPut", "api/{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            configuration.Routes.MapHttpRoute("DefaultApiDelete", "api/{controller}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            //configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler()); //Global handler - applicable to all the requests
            var container = new UnityContainer();
            container.RegisterType<ITokenServices, TokenServices>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<IUserService, UserServices>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<IDocumentRepository, DocumentRepository>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);

            configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}