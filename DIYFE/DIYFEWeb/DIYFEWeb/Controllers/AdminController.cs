using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIYFELib;

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

        public ActionResult AddArticle(Article article)
        {

            var data = new object();

            Article ac = new Article();
            if (ac.InsertArticle(article))
            {
                data = new { success = true };
            }
            else
            {
                data = new { success = false, message = "Failed to insert new article." };
                return Json(data);
            }

            return Json(data);
        }

        public ActionResult AddCategory(Category category)
        {

            var data = new object();

            Category cat = new Category();
            if (cat.InsertCategory(category))
            {
                AppStatic.LoadStaticCache();
                data = new { success = true };
            }
            else
            {
                data = new { success = false, message = "Failed to insert new category." };
                return Json(data);
            }

            return Json(data);
        }


    }
}
