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
            ProjectListModel model = new ProjectListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            ListAccess la = new ListAccess();
            model.MostViewed = la.MostViewed(11, 20);

            string url = HttpContext.Request.RawUrl;
            int catigoryRowId = DIYFEHelper.GetCatigoryRowId(url);

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryRowId, linkPrefix);
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

            ListAccess la = new ListAccess();
            ProjectListModel model = new ProjectListModel();
            model.MostViewed = la.MostViewed(11, 20);
            //model.CrumbLinkList = new List<CustomHtmlLink>();
            //AppStatic.Categories.Where(c => c.CategoryUrl == categoryUrl);
            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            string url = HttpContext.Request.RawUrl;
            int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(categoryRowId, linkPrefix);
            
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.CategoryRowId == categoryRowId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.CategoryRowId == categoryRowId).OrderBy(a => a.CreatedDate).ToList();
            }

            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/
        [LoggingFilter]
        public ActionResult SecondLevCategoryList(string categoryUrl, string subCategoryUrl)
        {

            string url = HttpContext.Request.RawUrl;
            int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, "");

            ListAccess la = new ListAccess();
            ProjectListModel model = new ProjectListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewSecondLev(categoryRowId, linkPrefix);
            
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.CategoryRowId == categoryRowId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.CategoryRowId == categoryRowId).OrderBy(a => a.CreatedDate).ToList();
            }
            
            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/asdf
        [LoggingFilter]
        public ActionResult ThirdLevCategoryList(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl)
        {
            string url = HttpContext.Request.RawUrl;
            int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            ListAccess la = new ListAccess();
            ProjectListModel model = new ProjectListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(categoryRowId, linkPrefix);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2 && a.CategoryRowId == categoryRowId).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 1 && a.CategoryRowId == categoryRowId).OrderBy(a => a.CreatedDate).ToList();
            }

           // model.ArticleList = la.ar PostList(catigoryRowId, 1, 200, 3);

            return View(model);
        }

        [LoggingFilter]
        public ActionResult ProjectDetails(string html)
        {
            int categoryRowId = 0;

            ListAccess la = new ListAccess();

            ArticleModel model = new ArticleModel();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Where(a => a.NameId == html).FirstOrDefault();
            }

            model.Comments = la.ArticleComments(model.Article.ArticleId);

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(categoryRowId, linkPrefix);

            PageModel.Title = model.Article.Title;
            PageModel.Description = model.Article.Description;
            PageModel.Author = model.Article.Author;
            PageModel.Keywords = model.Article.Keywords;

            return View(model);
        }


    }
}
