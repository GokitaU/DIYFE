using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFE.Web.Models;
using DIYFE.Web.Code;
using DIYFELib;


namespace DIYFE.Web.Controllers
{
    public class PostController : ApplicationController
    {
        string linkPrefix = "post";
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
            int catigoryRowId = DIYFEHelper.GetCatigoryRowId(url);

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryRowId, linkPrefix);
            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        //http://localhost:5808/project/subsomething
        [LoggingFilter]
        public ActionResult FirstLevCategoryList(string categoryUrl)
        {

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();
            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;


            model.MostViewed = la.MostViewed(11, 20);
            //model.CrumbLinkList = new List<CustomHtmlLink>();
            //AppStatic.Categories.Where(c => c.CategoryUrl == categoryUrl);

            string url = HttpContext.Request.RawUrl;
            int catigoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "","");

            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(catigoryRowId, linkPrefix);
            model.ArticleList = la.ArticleList(catigoryRowId, 1);
            model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(catigoryRowId, linkPrefix);

            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/
        [LoggingFilter]
        public ActionResult SecondLevCategoryList(string categoryUrl, string subCategoryUrl)
        {
            string url = HttpContext.Request.RawUrl;
            Category cat = DIYFEHelper.GetCategory(categoryUrl, subCategoryUrl, "");
            //int catigoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, "");

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat.CategoryRowId, linkPrefix);
            model.ArticleList = la.ArticleList(cat.CategoryRowId, 1);
           
            //model.RelatedTreeView = DIYFEHelper.GenerateRelatedTreeView(catigoryId, linkPrefix);
            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewSecondLev(cat.CategoryId, linkPrefix);
            return View(model);
        }

        //http://localhost:5808/project/subsomething/asdf/asdf
        [LoggingFilter]
        public ActionResult ThirdLevCategoryList(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl)
        {

            string url = HttpContext.Request.RawUrl;
            //int catigoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, subCategoryUrl, subSubCategoryUrl);
            Category cat = DIYFEHelper.GetCategory(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            ListAccess la = new ListAccess();
            ArticleModel model = new ArticleModel();
            
            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            model.MostViewed = la.MostViewed(11, 20);
            model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(cat.CategoryRowId, linkPrefix);
            model.ArticleList = la.ArticleList(cat.CategoryRowId, 1);

            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(cat.SecondLevCategoryId, linkPrefix);

            return View(model);
        }

        [LoggingFilter]
        public ActionResult PostDetails(string html)
        {
            int _categoryId = 0;

            ListAccess la = new ListAccess();

            ArticleModel model = new ArticleModel();
            model.ArticleContent.LoadArticle(html);
            model.Comments = la.ArticleComments(model.ArticleContent.ArticleId);

            if (model.ArticleContent.CategoryId > 0)
            { _categoryId = model.ArticleContent.CategoryId;
            }
            else if (model.ArticleContent.SecondLevCategoryId > 0)
            {_categoryId = model.ArticleContent.SecondLevCategoryId;
            }
            else
            {
                _categoryId = model.ArticleContent.ThirdLevCategoryId;
            }

            PageModel.ArticleContent.Title = model.ArticleContent.Title;
            PageModel.ArticleContent.Description = model.ArticleContent.Description;
            PageModel.ArticleContent.Author = model.ArticleContent.Author;
            PageModel.ArticleContent.Keywords = model.ArticleContent.Keywords;

            model.MostViewed = la.MostViewed(_categoryId, 20);

            model.RelatedTreeView = DIYFEHelper.GenerateTreeViewSecondLev(_categoryId, linkPrefix);

            return View(model);
        }



    }
}
