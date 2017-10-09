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
                defaults: new { controller = "Books", action = "Status" }
            );

            routes.MapRoute(
                name: "Books",
                url: "books/details/{id}",
                defaults: new { controller = "Books", action = "Details" }
            );

            routes.MapRoute(
                name: "ProfileDetails",
                url: "profile/details/{username}",
                defaults: new { controller = "Profile", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
