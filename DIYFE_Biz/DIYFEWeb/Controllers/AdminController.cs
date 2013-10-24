using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFE.EF;

namespace DIYFEWeb.Controllers
{
    public class AdminController : ApplicationController
    {
        public ActionResult Index()
        {
            DIYFEWeb.Models.AdminModel model = new DIYFEWeb.Models.AdminModel();
            model.Categories = StaticConfig.Categories;

            return View(model);
        }

        public ActionResult AddArticle(Article article)
        {

            var data = new object();

            article.CreatedDate = DateTime.Now;
            article.UpdateDate = DateTime.Now;

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                }
                StaticConfig.LoadStaticCache();
                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

    }
}
