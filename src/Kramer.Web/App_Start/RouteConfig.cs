using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kramer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           routes.MapRoute(
                name: "Requests", //esse é um nome qualquer que você dá para a sua rota,
                url: "requests",
                defaults: new {controller = "UserRequests", action = "Index"});

           routes.MapRoute(
               name: "Home",
               url: "Home",
               defaults: new { controller = "UserRequests", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "UserRequests", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
