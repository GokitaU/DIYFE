using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIYFEWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PostArticle",
                url: "post/{URL1}/{URL2}/{URL3}/{URL4}",
                defaults: new
                {
                    controller = "Article",
                    action = "ArticleDetails",
                    URL1 = UrlParameter.Optional,
                    URL2 = UrlParameter.Optional,
                    URL3 = UrlParameter.Optional,
                    URL4 = UrlParameter.Optional
                },
                constraints: new { URL1 = @"*.html", URL2 = @"*.html", URL3 = @"*.html", URL4 = @"*.html" } 
            );
            
            routes.MapRoute(
                name: "Post",
                url: "post/{URL1}/{URL2}/{URL3}",
                defaults: new { controller = "Article", action = "ArticleList",
                                URL1 = UrlParameter.Optional,
                                URL2 = UrlParameter.Optional,
                                URL3 = UrlParameter.Optional
                }
            );

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}