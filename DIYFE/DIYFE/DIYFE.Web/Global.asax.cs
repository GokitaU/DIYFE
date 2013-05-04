using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace DIYFE.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
      

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "DefaultSportsNBA", // Route name
            //    "sports-betting/{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Sports", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Categories",
            //    "Categories/{*category}",
            //    new { controller = "Categories", action = "Details" });

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
            routes.MapRoute(
                "ProjectCategory", // Route name
                "Project/{categoryUrl}", // URL with parameters
                new { controller = "Project", action = "FirstLevCategoryList", categoryUrl = "" }
            );
            routes.MapRoute(
                "ProjectContent1", // Route name
                "Project/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            //----------------------
            routes.MapRoute(
                "ProjectSubCategory", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}", // URL with parameters
                new { controller = "Project", action = "SecondLevCategoryList", categoryUrl = "", subCategoryUrl = "" } // Parameter defaults
            );
            routes.MapRoute(
                "ProjectContent2", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Project", action = "ProjectDetails" }//, // Parameter defaults
            );

            //-------------------
            routes.MapRoute(
                "ProjectSubSubCategory", // Route name
                "Project/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new { controller = "Project", action = "ThirdLevCategoryList", categoryUrl = "", subCategoryUrl = "", subSubCategoryUrl = "" } // Parameter defaults
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
            routes.MapRoute(
                "PostCategory", // Route name
                "Post/{categoryUrl}", // URL with parameters
                new { controller = "Post", action = "FirstLevCategoryList", categoryUrl = "" }
            );

            routes.MapRoute(
                "PostContent1", // Route name
                "Post/{categoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            //----------------------
            routes.MapRoute(
                "PostSubCategory", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}", // URL with parameters
                new { controller = "Post", action = "SecondLevCategoryList", categoryUrl = "", subCategoryUrl = "" } // Parameter defaults
            );
            routes.MapRoute(
                "PostContent2", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            //-------------------
            routes.MapRoute(
                "PostSubSubCategory", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}", // URL with parameters
                new { controller = "Post", action = "ThirdLevCategoryList", categoryUrl = "", subCategoryUrl = "", subSubCategoryUrl = "" } // Parameter defaults
            );
            routes.MapRoute(
                "PostContent3", // Route name
                "Post/{categoryUrl}/{subCategoryUrl}/{subSubCategoryUrl}/{html}.html", // URL with parameters,
                new { controller = "Post", action = "PostDetails" }//, // Parameter defaults
            );

            #endregion

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Sports", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AppStatic.LoadStaticCache();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        //protected void Session_Start(object sender, EventArgs e)
        //{
        //    DIYFELib.Tracking.InsertTracking(HttpContext.Current.Session.SessionID,
        //                                    "104.23.322.323", 
        //                                    "fax/asd/asdf/");

        //    string sessionId = HttpContext.Current.Session.SessionID;
        //}

        //void Session_End(object sender, EventArgs e)
        //{ 
        //}

    }
}