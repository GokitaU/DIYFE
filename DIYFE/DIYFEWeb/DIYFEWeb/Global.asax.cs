using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace DIYFEWeb
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

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            AppStatic.LoadStaticCache();

            RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterRoutes(RouteTable.Routes);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);

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