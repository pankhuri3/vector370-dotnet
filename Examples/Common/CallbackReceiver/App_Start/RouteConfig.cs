using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace CallbackReceiver
{
    /// <summary />
    public class RouteConfig
    {
        /// <summary />
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
