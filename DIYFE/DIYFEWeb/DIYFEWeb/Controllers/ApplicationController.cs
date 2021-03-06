﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;

namespace DIYFEWeb.Controllers
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
            string _sessionId;
            _ipAddress = ctx.HttpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(_ipAddress))
            { _ipAddress = ctx.HttpContext.Request.ServerVariables["REMOTE_ADDR"]; }
            if (Request.Cookies["sessionKey"] != null)
            {
                _sessionId = Request.Cookies["sessionKey"].Value;
            }
            else
            {
                Response.Cookies.Add(new System.Web.HttpCookie("sessionKey", ctx.HttpContext.Session.SessionID));
                Response.Cookies["sessionKey"].Expires = DateTime.Now.AddHours(12);
                _sessionId = ctx.HttpContext.Session.SessionID;
            }

            DIYFELib.Tracking.InsertTracking(_sessionId,
                                            _ipAddress,
                                            ctx.HttpContext.Request.Url.PathAndQuery);

            ViewBag.PageModel = PageModel;
        }


        //protected override void OnResultExecuted(ResultExecutedContext ctx)
        //{
        //    base.OnResultExecuted(ctx);

        //    //string sDbg = ctx.Controller.TempData["DebugTrc"] as string;
        //    //System.Diagnostics.Debug.WriteLine("OnResultExecuted " +
        //    //sDbg);
        //}

        

    }
}
