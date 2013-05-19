using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFEWeb.Controllers
{
    public class AdminController : ApplicationController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            DIYFEWeb.Models.AdminModel model = new DIYFEWeb.Models.AdminModel();
            model.Categories = AppStatic.Categories;

            PageModel.ArticleContent.Title = "";
            PageModel.ArticleContent.Description = "";
            PageModel.ArticleContent.Author = "";
            PageModel.ArticleContent.Keywords = "";

            return View(model);
        }



    }
}
