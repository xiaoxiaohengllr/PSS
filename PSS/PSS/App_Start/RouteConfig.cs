using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PSS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Action1Html", // action伪静态    
               "{controller}/{action}.html",// 带有参数的 URL    
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }// 参数默认值    
            );
            routes.MapRoute(
               "IDHtml", // id伪静态    
               "{controller}/{action}/{id}.html",// 带有参数的 URL    
               new { controller = "Main", action = "Index", id = UrlParameter.Optional }// 参数默认值    
            );

            routes.MapRoute(
                "ActionHtml", // action伪静态    
                "{controller}/{action}.html/{id}",// 带有参数的 URL    
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }// 参数默认值    
            );

            routes.MapRoute(
                "ControllerHtml", // controller伪静态    
                "{controller}.html/{action}/{id}",// 带有参数的 URL    
                new { controller = "Main", action = "Index", id = UrlParameter.Optional }// 参数默认值    
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
