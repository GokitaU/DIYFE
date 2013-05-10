using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIYFE.Web.Controllers
{
    public class AdminController : ApplicationController
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            DIYFE.Web.Models.AdminModel model = new DIYFE.Web.Models.AdminModel();
            model.Categories = AppStatic.Categories;

            PageModel.ArticleContent.Title = "";
            PageModel.ArticleContent.Description = "";
            PageModel.ArticleContent.Author = "";
            PageModel.ArticleContent.Keywords = "";

            return View(model);
        }



    }
}
