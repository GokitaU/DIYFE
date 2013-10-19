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

            routes.MapRoute("ArticleDeaitls", "{articleType}/{articleName}.html",
                new
                {
                    controller = "Article",
                    action = "ArticleDetails",
                },
                new { articleType = @"(post|project|blog|news)" });

            routes.MapRoute("ArticleDeaitls1", "{articleType}/{categoryUrl}/{articleName}.html",
                new
                {
                    controller = "Article",
                    action = "ArticleDetails"
                },
                new { articleType = @"(post|project|blog|news)" });

            routes.MapRoute("ArticleDeaitls2", "{articleType}/{categoryUrl}/{subCategoryUrl}/{articleName}.html",
                new
                {
                    controller = "Article",
                    action = "ArticleDetails"
                },
                new { articleType = @"(post|project|blog|news)" });

            routes.MapRoute("ArticleDeaitls3", "{articleType}/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{articleName}.html",
                new
                {
                    controller = "Article",
                    action = "ArticleDetails"
                },
                new { articleType = @"(post|project|blog|news)" });

            routes.MapRoute("ArticleList", "{articleType}/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}",
                new
                {
                    controller = "Article",
                    action = "ArticleList",
                    categoryUrl = "",
                    subCategoryUrl = "",
                    subSubCategoryUrl = ""
                },
                new { articleType = @"(post|project|blog|news)" });
            //#region ProjectMaps

            //routes.MapRoute(
            //    "ProjectRoot", // Route name
            //    "Project", // URL with parameters
            //    new { controller = "Article", action = "ArticleList" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "ProjectContent", // Route name
            //    "Project/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);


            //routes.MapRoute(
            //    "ProjectContent1", // Route name
            //    "Project/{categoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);


            //routes.MapRoute(
            //    "ProjectContent2", // Route name
            //    "Project/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            ////-------------------
            //routes.MapRoute(
            //    "ProjectSubSubCategory", // Route name
            //    "Project/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
            //    new { controller = "Article", action = "ArticleList", categoryUrl = "", subCategoryUrl = "", subSubCategoryUrl = "" } // Parameter defaults
            //);
            //routes.MapRoute(
            //    "ProjectContent3", // Route name
            //    "Project/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //#endregion

            #region PostMaps
            routes.MapRoute(
                "PostSubSubCategoryTEST", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new
                {
                    controller = "Article",
                    action = "ArticleList",
                    categoryUrl = UrlParameter.Optional,
                    subCategoryUrl = UrlParameter.Optional,
                    subSubCategoryUrl = UrlParameter.Optional
                } // Parameter defaults
            );



            routes.MapRoute(
                "PostRoot", // Route name
                "Post", // URL with parameters
                new { controller = "Article", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "PostContent", // Route name
                "Post/{html}.html", // URL with parameters,
                new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "PostContent1", // Route name
                "Post/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            );

            routes.MapRoute(
                "PostContent2", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            );

            //-------------------
            routes.MapRoute(
                "PostSubSubCategory", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new { controller = "Article", action = "ArticleList", 
                    categoryUrl = "", 
                    subCategoryUrl = "", 
                    subSubCategoryUrl = "" } // Parameter defaults
            );

            routes.MapRoute(
                "PostContent3", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            );

            #endregion

            //#region BlogMaps

            //routes.MapRoute(
            //    "BlogRoot", // Route name
            //    "Blog", // URL with parameters
            //    new { controller = "Article", action = "ArticleList" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "BlogContent", // Route name
            //    "Blog/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "BlogContent1", // Route name
            //    "Blog/{categoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "BlogContent2", // Route name
            //    "Blog/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "BlogContent3", // Route name
            //    "Blog/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleList" }//, // Parameter defaults
            //);

            //#endregion

            //#region NewsMaps

            //routes.MapRoute(
            //    "NewsRoot", // Route name
            //    "News", // URL with parameters
            //    new { controller = "Article", action = "ArticleList" } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "NewsContent", // Route name
            //    "News/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "NewsContent1", // Route name
            //    "News/{categoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "NewsContent2", // Route name
            //    "News/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //routes.MapRoute(
            //    "NewsContent3", // Route name
            //    "News/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
            //    new { controller = "Article", action = "ArticleDetails" }//, // Parameter defaults
            //);

            //#endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}