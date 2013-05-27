using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;
using DIYFEWeb.Code;
using DIYFELib;

namespace DIYFEWeb.Controllers
{

    public class ProjectController : ApplicationController
    {
        string linkPrefix = "project";
        //
        // GET: /Project/
        [LoggingFilter]
        public ActionResult Index()
        {
            ArticleModel model = new ArticleModel();
            model.ArticleContent.LoadArticle(5);

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            ListAccess la = new ListAccess();
            model.MostViewed = la.MostViewed(11, 20);

            string url = HttpContext.Request.RawUrl;
            int catigoryId = DIYFEHelper.GetCatigoryRowId(url);

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryId, linkPrefix);
            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        //http://localhost:5808/project/subsomething
        [LoggingFilter]
        public ActionResult FirstLevCategoryList(string categoryUrl)
        {

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();
            model.MostViewed = la.MostViewed(11, 20);
            //model.CrumbLinkList = new List<CustomHtmlLink>();
            //AppStatic.Categories.Where(c => c.CategoryUrl == categoryUrl);
            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            string url = HttpContext.Request.RawUrl;
            int catigoryId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryId, linkPrefix);
            model.ArticleList = la.ArticleList(catigoryId, 1);
            // model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(catigoryId, linkPrefix);

            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/
        [LoggingFilter]
        public ActionResult SecondLevCategoryList(string categoryUrl, string subCategoryUrl)
        {

            string url = HttpContext.Request.RawUrl;
            int catigoryId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, "");

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryId, linkPrefix);
            model.ArticleList = la.ArticleList(catigoryId, 1);
            //model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(catigoryId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewSecondLev(catigoryId, linkPrefix);
            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/asdf
        [LoggingFilter]
        public ActionResult ThirdLevCategoryList(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl)
        {
            string url = HttpContext.Request.RawUrl;
            int catigoryId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryId, linkPrefix);
            model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        [LoggingFilter]
        public ActionResult ProjectDetails(string html)
        {
            int articleId = 2;

            ListAccess la = new ListAccess();

            ArticleModel model = new ArticleModel();
            model.ArticleContent.LoadArticle(html);
            model.Comments = la.ArticleComments(articleId);

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;


            return View(model);
        }


    }
}
