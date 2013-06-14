using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFEWeb.Controllers
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
                filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuting(filterContext);
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