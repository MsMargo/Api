using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MedicalApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
            name: "Administration",
            url: "Administration/GetPartialView",
            defaults: new { controller = "Administration", action = "GetPartialView" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Administration", action = "Reception", id = UrlParameter.Optional }
            );
        }
    }
}
