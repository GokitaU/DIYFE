﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFE.Web.Controllers
{
    public class NewsController : ApplicationController
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

    }
}
