using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC3Template.Models;

namespace MVC3Template.Controllers
{
    public class PartialController : Controller
    {
        public ActionResult LogonForm()
        {
            LogOnModel model = new LogOnModel();

            return PartialView("_LogonForm", model);
        }

    }
}
