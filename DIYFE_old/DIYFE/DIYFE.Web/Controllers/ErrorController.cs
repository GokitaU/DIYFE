using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFE.Web.Controllers
{
    public class ErrorController : Controller
    {

        public ActionResult ServerError404(string aspxerrorpath)
        {
            ViewBag.ErrorMessage = "The page you requested was not found";
            return View("ServerError");
        }

        public ActionResult ServerError500(string aspxerrorpath)
        {
            ViewBag.ErrorMessage = "There was an error with out servers.";
            return View("ServerError");
        }

        // (optional) Redirect to home when /Error is navigated to directly  
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
    }

    
}
