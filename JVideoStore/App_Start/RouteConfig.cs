using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace JVideoStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "MoviesByReleaseDate",
                "Movies/Released/{year}/{month}",
                new {Controller="Movies", Action = "ByReleaseDate"},
                new { year = @"\d{4}", month = @"\d{2}" }
                );

            //routes.MapRoute(
            //       name: "Customers",
            //       url: "Customers",
            //       defaults: new { controller = "Home", action = "Customers" }
            //   );
            //routes.MapRoute(
            //       name: "Movies",
            //       url: "Movies",
            //       defaults: new { controller = "Home", action = "Random" }
            //   );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
