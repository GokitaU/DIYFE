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
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            DIYFEWeb.Models.AdminModel model = new DIYFEWeb.Models.AdminModel();
            model.Categories = AppStatic.Categories;

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            return View(model);
        }

        public ActionResult AddArticelTest(dynamic article)
        {
            var data = new object();
            //Article article = (Article)articleDynamic;
            //article.CreatedDate = DateTime.Now;
            //article.UpdateDate = DateTime.Now;

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                }
                AppStatic.LoadStaticCache();
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
                AppStatic.LoadStaticCache();
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

        public ActionResult AddCategory(DIYFE.EF.Category category)
        {

            var data = new object();

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                if (category.ThirdLevCategoryId == 0 && category.SecondLevCategoryId != 0)
                {
                    category.ThirdLevCategoryId = db.Categories.Max(c => c.ThirdLevCategoryId).Value;
                }
                if (category.SecondLevCategoryId == 0)
                {
                    category.SecondLevCategoryId = db.Categories.Max(c => c.SecondLevCategoryId).Value;
                }
                if (category.CategoryId == 0)
                {
                    category.CategoryId = db.Categories.Max(c => c.CategoryId);
                }
                db.Categories.Add(category);
                db.SaveChanges();
                data = new { success = true };
                //allCats = db.Categories.ToList();
            }

            AppStatic.LoadStaticCache();

            //Category cat = new Category();
            //if (cat.InsertCategory(category))
            //{
            //    AppStatic.LoadStaticCache();
            //    data = new { success = true };
            //}
            //else
            //{
            //    data = new { success = false, message = "Failed to insert new category." };
            //    return Json(data);
            //}

            return Json(data);
        }


    }
}
