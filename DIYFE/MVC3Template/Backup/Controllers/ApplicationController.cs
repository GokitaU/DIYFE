using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC3Template.Models;

namespace MVC3Template.Controllers
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
