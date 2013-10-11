using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;

using DIYFEWeb.Models;
using DIYFEWeb.Code;
using DIYFE.EF;

namespace DIYFEWeb.Controllers
{
 
    public class NewsController : ApplicationController
    {
        //
        // GET: /News/
        string linkPrefix = "news";
        private int pageSize = 10;

        [LoggingFilter]
        public ActionResult Index(int? page)
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
               // model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 4).OrderBy(a => a.CreatedDate).ToList();
                model.PagedArticle = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 4).OrderBy(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
            }

            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        [LoggingFilter]
        public ActionResult NewsDetails(string html)
        {
            int categoryRowId = 0;
            Category cat = new Category();
            ArticleModel model = new ArticleModel();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Include("ArticleComments").Where(a => a.URLLink == html + ".html").FirstOrDefault();
            }

            if (model.Article == null)
            {
                cat = DIYFEHelper.GetCatigroy(model.Article.CategoryRowId);
                model.Article = new DIYFE.EF.Article();
                model.Comments = new List<DIYFE.EF.ArticleComment>();
                //model.Article.ArticleComments = new List<ArticleComment>();
            }
            else
            {
                DIYFELib.Tracking.InsertArticleViewRequest(model.Article.ArticleId);
            }


            // model.Comments = la.ArticleComments(model.Article.ArticleId);

            //model.MostViewed = la.MostViewed(11, 20);
            // model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
             model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(cat, linkPrefix);

            ////PageModel.Title = model.Article.Title;
            ////PageModel.Description = model.Article.Description;
            ////PageModel.Author = model.Article.Author;
            ////PageModel.Keywords = model.Article.Keywords;

            return View(model);
        }
    }
}
