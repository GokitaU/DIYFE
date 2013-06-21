using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFEWeb.Controllers
{
    public class BlogController : ApplicationController
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                var articles = db.Articles.Where(a => a.ArticleTypeId == 3);

            }

            return View();
        }

    }
}
