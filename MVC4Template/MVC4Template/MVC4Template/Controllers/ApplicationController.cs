﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC4Template.Models;

namespace MVC4Template.Controllers
{
    [HandleError(View = "DefaultError")]
    public abstract class ApplicationController : Controller
    {
        //***************?????????????***********************///
        //NOTE:PUT SHARED DATA CONTEXT HERE
        //private ProjectDataContext datacontext = new ProjectDataContext();

        //protected ProjectDataContext DataContext
        //{
        //    get { return datacontext; }
        //}

        private PageBaseModel pageBaseModel = new PageBaseModel();
        protected PageBaseModel PageModel
        {
            get { return pageBaseModel; }
        }

        public ApplicationController()
        {
           
        }

        //protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //    //ViewData["OpenTasks"] = DataContext.Tasks.Where(t => t.UserId == this.UserId).Count();
        //}

        protected override void OnResultExecuting(ResultExecutingContext ctx)
        {
            base.OnResultExecuting(ctx);

            //string sDbg = ctx.Controller.TempData["DebugTrc"] as string;
            //System.Diagnostics.Debug.WriteLine("OnResultExecuted " +
            //sDbg);

            string _ipAddress;
            _ipAddress = ctx.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(_ipAddress))
            { _ipAddress = ctx.HttpContext.Request.ServerVariables["REMOTE_ADDR"]; }

            //DIYFELib.Tracking.InsertTracking(ctx.HttpContext.Session.SessionID,
            //                                _ipAddress,
            //                                ctx.HttpContext.Request.Url.PathAndQuery);

            ViewBag.PageModel = PageModel;
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //********************************?????????????************************///
            //NOTE:PUT ERROR LOGGING CODE HERE
            if (filterContext.Exception != null)
            {
                MVC4Template.Models.ErrorModel err = new MVC4Template.Models.ErrorModel();
                err.InsertError(filterContext.Exception);

                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");
            }

            //****************************????********************///
            //NOTE: PUT USER TRACKING CODE HERE

            base.OnActionExecuted(filterContext);
        }
        

    }
}
