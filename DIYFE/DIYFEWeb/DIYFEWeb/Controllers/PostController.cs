using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;
using DIYFEWeb.Code;
using DIYFE.EF;


namespace DIYFEWeb.Controllers
{
    
    public class PostController : ApplicationController
    {
        string linkPrefix = "post";
        //
        // GET: /Project/
        [LoggingFilter]
        public ActionResult Index()
        {
            ArticleListModel model = new ArticleListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            //ListAccess la = new ListAccess();
            //model.MostViewed = la.MostViewed(11, 20);

            string url = HttpContext.Request.RawUrl;
            //int catigoryRowId = DIYFEHelper.GetCatigoryRowId(url);

            //model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryRowId, linkPrefix);
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1).OrderBy(a => a.CreatedDate).ToList();
            }

            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        //http://localhost:5808/project/subsomething
        [LoggingFilter]
        public ActionResult FirstLevCategoryList(string categoryUrl)
        {
            ProjectListModel model = new ProjectListModel();
            //model.MostViewed = la.MostViewed(11, 20);
            //model.CrumbLinkList = new List<CustomHtmlLink>();
            //AppStatic.Categories.Where(c => c.CategoryUrl == categoryUrl);
            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            string url = HttpContext.Request.RawUrl;
            //int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");
            Category cat = DIYFEHelper.GetCatigory(categoryUrl, "", "");

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat, linkPrefix);
           // model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(cat, linkPrefix);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.Category.CategoryId == cat.CategoryId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.Category.CategoryId == cat.CategoryId).OrderBy(a => a.CreatedDate).ToList();
            }

            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/
        [LoggingFilter]
        public ActionResult SecondLevCategoryList(string categoryUrl, string subCategoryUrl)
        {

            string url = HttpContext.Request.RawUrl;
            Category cat = DIYFEHelper.GetCatigory(categoryUrl, subCategoryUrl, "");

            ProjectListModel model = new ProjectListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            //model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat, linkPrefix);
           // model.RelatedTreeView = DIYFEHelper.GenerateTreeViewSecondLev(cat, linkPrefix);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.CategoryRowId == cat.CategoryRowId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.Category.SecondLevCategoryId == cat.SecondLevCategoryId).OrderBy(a => a.CreatedDate).ToList();
            }

            return View("FirstLevCategoryList", model);
        }

        //http://localhost:5808/project/subsomething/asdf/asdf
        [LoggingFilter]
        public ActionResult ThirdLevCategoryList(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl)
        {
            string url = HttpContext.Request.RawUrl;
            Category cat = DIYFEHelper.GetCatigory(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            ProjectListModel model = new ProjectListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

           // model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat, linkPrefix);
            //model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(cat, linkPrefix);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.CategoryRowId == cat.CategoryRowId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.Category.ThirdLevCategoryId == cat.ThirdLevCategoryId).OrderBy(a => a.CreatedDate).ToList();
            }

            // model.ArticleList = la.ar PostList(catigoryRowId, 1, 200, 3);

            return View("FirstLevCategoryList", model);
        }

        [LoggingFilter]
        public ActionResult PostDetails(string html)
        {
            int categoryRowId = 0;

            ArticleModel model = new ArticleModel();
            Category cat = new Category();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Include("ArticleComment").Where(a => a.URLLink == html + ".html").FirstOrDefault();
            }

            if (model.Article != null)
            {
                cat = DIYFEHelper.GetCatigroy(model.Article.CategoryRowId);

                // model.Comments = la.ArticleComments(model.Article.ArticleId);

                // model.MostViewed = la.MostViewed(11, 20);
                model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat, linkPrefix);
                //model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(model.Article.CategoryRowId, linkPrefix);

                PageModel.Title = model.Article.Title;
                PageModel.Description = model.Article.Description;
                PageModel.Author = model.Article.Author;
                PageModel.Keywords = model.Article.Keywords;
            }
            else
            {
                //JUST TO GET BY -- REPLACE WITH 404 SOULATION
                model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(new Category(), linkPrefix);
                model.Article = new Article();
                model.Comments = new List<ArticleComment>();
                //model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(model.Article.CategoryRowId, linkPrefix);

                PageModel.Title = "";
                PageModel.Description = "";
                PageModel.Author = "";
                PageModel.Keywords = "";
            }
            

           // model.Comments = la.ArticleComments(model.Article.ArticleId);

            //model.MostViewed = la.MostViewed(11, 20);
           // model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
           // model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(cat, linkPrefix);

            ////PageModel.Title = model.Article.Title;
            ////PageModel.Description = model.Article.Description;
            ////PageModel.Author = model.Article.Author;
            ////PageModel.Keywords = model.Article.Keywords;

            return View(model);
        }



    }
}
