﻿using Owin;
using System.Web.Http;
using MunicipalityTaxCalculator.Controllers;

namespace MunicipalityTaxCalculator
{
    class SelfHostConfig
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = "get", id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
                name: "SecondaryApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new AllExceptionFilterAttribute());

            appBuilder.UseWebApi(config);
        }
    }
}
