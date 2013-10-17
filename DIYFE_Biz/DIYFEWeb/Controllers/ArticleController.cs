using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4Template.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArticleList(string URL1, string URL2, string URL3)
        {
            return View();
        }

        public ActionResult ArticleDetails(string URL1, string URL2, string URL3, string URL4)
        {
            return View();
        }

    }
}
