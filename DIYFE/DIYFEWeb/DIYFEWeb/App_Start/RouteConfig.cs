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
                name: "WeeklyMenu",
                url: "WeeklyMenu",
                defaults: new { controller = "Home", action = "WeeklyMenu" }
            );

            #region ProjectMaps

            routes.MapRoute(
                "ProjectRoot", // Route name
                "Project", // URL with parameters
                new { controller = "Project", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "ProjectContent", // Route name
                "Project/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            //---------------------------------------
            //routes.MapRoute(
            //    "ProjectCategory", // Route name
            //    "Project/{categoryUrl}", // URL with parameters
            //    new { controller = "Project", action = "FirstLevCategoryList", categoryUrl = "" }
            //);
            routes.MapRoute(
                "ProjectContent1", // Route name
                "Project/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            //----------------------
            //routes.MapRoute(
            //    "ProjectSubCategory", // Route name
            //    "Project/{categoryUrl}/{subCategoryUrl}", // URL with parameters
            //    new { controller = "Project", action = "SecondLevCategoryList", categoryUrl = "", subCategoryUrl = "" } // Parameter defaults
            //);
            routes.MapRoute(
                "ProjectContent2", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            //-------------------
            routes.MapRoute(
                "ProjectSubSubCategory", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new { controller = "Project", action = "FirstLevCategoryList", categoryUrl = "", subCategoryUrl = "", subSubCategoryUrl = "" } // Parameter defaults
            );
            routes.MapRoute(
                "ProjectContent3", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            #endregion

            #region PostMaps

            routes.MapRoute(
                "PostRoot", // Route name
                "Post", // URL with parameters
                new { controller = "Post", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "PostContent", // Route name
                "Post/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            //---------------------------------------
            //routes.MapRoute(
            //    "PostCategory", // Route name
            //    "Post/{categoryUrl}", // URL with parameters
            //    new { controller = "Post", action = "FirstLevCategoryList", categoryUrl = "" }
            //);

            routes.MapRoute(
                "PostContent1", // Route name
                "Post/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            //----------------------
            //routes.MapRoute(
            //    "PostSubCategory", // Route name
            //    "Post/{categoryUrl}/{subCategoryUrl}", // URL with parameters
            //    new { controller = "Post", action = "SecondLevCategoryList", categoryUrl = "", subCategoryUrl = "" } // Parameter defaults
            //);
            routes.MapRoute(
                "PostContent2", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            //-------------------
            routes.MapRoute(
                "PostSubSubCategory", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new { controller = "Post", action = "FirstLevCategoryList", categoryUrl = "", subCategoryUrl = "", subSubCategoryUrl = "" } // Parameter defaults
            );
            routes.MapRoute(
                "PostContent3", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            #endregion

            #region BlogMaps

            routes.MapRoute(
                "BlogRoot", // Route name
                "Blog", // URL with parameters
                new { controller = "Blog", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "BlogContent", // Route name
                "Blog/{html}.html", // URL with parameters,
                new { controller = "Blog", action = "BlogDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "BlogContent1", // Route name
                "Blog/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Blog", action = "BlogDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "BlogContent2", // Route name
                "Blog/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Blog", action = "BlogDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "BlogContent3", // Route name
                "Blog/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Blog", action = "BlogDetails" }//, // Parameter defaults
            );

            #endregion

            #region NewsMaps

            routes.MapRoute(
                "NewsRoot", // Route name
                "News", // URL with parameters
                new { controller = "News", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "NewsContent", // Route name
                "News/{html}.html", // URL with parameters,
                new { controller = "News", action = "NewsDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "NewsContent1", // Route name
                "News/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "News", action = "NewsDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "NewsContent2", // Route name
                "News/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "News", action = "NewsDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "NewsContent3", // Route name
                "News/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "News", action = "NewsDetails" }//, // Parameter defaults
            );

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Defaul1t",
                url: "{controller}/{action}/{id}/{id2}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );
        }
    }
}