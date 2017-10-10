using System.Web.Mvc;
using System.Web.Routing;

namespace ReadMe.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Status",
                url: "books/status",
                defaults: new { area = "", controller = "Books", action = "Status" },
                namespaces: new[] { "ReadMe.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Books",
                url: "books/details/{id}",
                defaults: new { area = "", controller = "Books", action = "Details" },
                namespaces: new[] { "ReadMe.Web.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Authors",
            //    url: "authors/details/{id}",
            //    defaults: new { area = "", controller = "Authors", action = "Details" },
            //    namespaces: new[] { "ReadMe.Web.Controllers" }
            //);

            routes.MapRoute(
                name: "ProfileDetails",
                url: "profile/details/{username}",
                defaults: new { controller = "Profile", action = "Details" },
                namespaces: new[] { "ReadMe.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ReadMe.Web.Controllers" }
            );
        }
    }
}
