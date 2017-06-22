using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VNW.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Login",
             url: "dang-nhap.html",
             defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
             namespaces: new string[] { "VNW.Web.Controllers" }
         );
            routes.MapRoute(
            name: "Register",
            url: "dang-ky.html",
            defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
            namespaces: new string[] { "VNW.Web.Controllers" }
        );

            routes.MapRoute(
           name: "LogOut",
           url: "thoat.html",
           defaults: new { controller = "Account", action = "LogOut", id = UrlParameter.Optional },
           namespaces: new string[] { "VNW.Web.Controllers" }
       );

            routes.MapRoute(
                name: "Search",
                url: "tim-kiem.html",
                defaults: new { controller = "TinTuyenDung", action = "Search", id = UrlParameter.Optional },
                namespaces: new string[] { "VNW.Web.Controllers" }
            );

            routes.MapRoute(
            name: "Detail",
            url: "{alias}-{id}.html",
            defaults: new { controller = "TinTuyenDung", action = "Detail", id = UrlParameter.Optional },
              namespaces: new string[] { "VNW.Web.Controllers" }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
