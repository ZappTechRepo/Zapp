using Homemation.WebAPI.Models;
using Homemation.WebAPI.Repository;
using Microsoft.Practices.Unity;
using ProductStore.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TriboschAdmin.API.Models;
using TriboschAdmin.API.Repositories;
using TriboschAdmin.WebAPI.Repository;

namespace TriboschAdmin.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });


            //configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler()); //Global handler - applicable to all the requests
            var container = new UnityContainer();
            container.RegisterType<ITokenServices, TokenServices>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<IUserService, UserServices>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);

            container.RegisterType<IDocumentRepository, DocumentRepository>(new HierarchicalLifetimeManager());
            configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}