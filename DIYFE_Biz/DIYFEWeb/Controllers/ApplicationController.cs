using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;

namespace DIYFEWeb.Controllers
{
    public class ApplicationController : Controller
    {
        //***************?????????????***********************///
        //NOTE:PUT SHARED DATA CONTEXT HERE
        //private ProjectDataContext datacontext = new ProjectDataContext();

        //protected ProjectDataContext DataContext
        //{
        //    get { return datacontext; }
        //}

        private ApplicationModel pageBaseModel = new ApplicationModel();
        protected ApplicationModel PageModel
        {
            get { return pageBaseModel; }
        }

        public ApplicationController()
        {

        }

        protected override void OnResultExecuting(ResultExecutingContext ctx)
        {
            base.OnResultExecuting(ctx);

            string _ipAddress;
            _ipAddress = ctx.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(_ipAddress))
            { _ipAddress = ctx.HttpContext.Request.ServerVariables["REMOTE_ADDR"]; }

            //DIYFELib.Tracking.InsertTracking(ctx.HttpContext.Session.SessionID,
            //                                _ipAddress,
            //                                ctx.HttpContext.Request.Url.PathAndQuery);

            ViewBag.PageModel = PageModel;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (filterContext.Exception != null)
            {
                DIYFEWeb.Models.ErrorModel err = new DIYFEWeb.Models.ErrorModel();
                err.InsertError(filterContext.Exception);

                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");
            }

            base.OnActionExecuted(filterContext);
        }

    }
}
