using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;
using DIYFELib;

namespace DIYFEWeb.Controllers
{
  
    public class ProductsController : ApplicationController
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            PageModel.Title = "DiyFe Products";
            PageModel.Description = "List of products dealing with ";
            PageModel.Author = "Do it yourself for everyone.";
            PageModel.Keywords = "DIY, DIYFE, do it yourself, homesteading, transition";

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();
            //model.MostViewed = la.MostViewed(11, 20);
            //model.CrumbLinkList = new List<CustomHtmlLink>();

            return View(model);
        }

    }
}
