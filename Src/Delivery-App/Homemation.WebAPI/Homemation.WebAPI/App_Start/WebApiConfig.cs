using Homemation.WebAPI.Models;
using Homemation.WebAPI.Repository;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Homemation.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new UnityContainer();
            //container.RegisterInstance<EntitiesConnectionValue>(new InjectionFactory(c => new EntitiesConnectionValue())); 

           
            container.RegisterType<IDocumentRepository, DocumentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserServices>(new HierarchicalLifetimeManager());
            container.RegisterType<ITokenServices, TokenServices>(new HierarchicalLifetimeManager());

            
            

            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            config.EnableCors();
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
