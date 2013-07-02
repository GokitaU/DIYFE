using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;
using DIYFELib;


namespace DIYFEWeb.Controllers
{
 
    public class NewsController : ApplicationController
    {
        //
        // GET: /News/

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
               // model.ProjectList = db.Articles.Where(a => a.ArticleTypeId == 2).OrderBy(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).ToList();
                model.ArticleList = db.Articles.Include("ArticleComment").Where(a => a.ArticleTypeId == 4).OrderBy(a => a.CreatedDate).ToList();
            }

            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

        [LoggingFilter]
        public ActionResult NewsDetails(string html)
        {
            int categoryRowId = 0;

            ListAccess la = new ListAccess();

            ArticleModel model = new ArticleModel();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Include("ArticleComment").Where(a => a.URLLink == html + ".html").FirstOrDefault();
            }

            if (model.Article == null)
            {
                model.Article = new DIYFE.EF.Article();
                model.Comments = new List<DIYFE.EF.ArticleComment>();
                //model.Article.ArticleComments = new List<ArticleComment>();
            }


            // model.Comments = la.ArticleComments(model.Article.ArticleId);

            //model.MostViewed = la.MostViewed(11, 20);
            // model.CrumbLinkList = DIYFEHelper.GenerateCrumbLinks(categoryRowId, linkPrefix);
            // model.RelatedTreeView = DIYFEHelper.GenerateTreeViewThirdLev(categoryRowId, linkPrefix);

            ////PageModel.Title = model.Article.Title;
            ////PageModel.Description = model.Article.Description;
            ////PageModel.Author = model.Article.Author;
            ////PageModel.Keywords = model.Article.Keywords;

            return View(model);
        }
    }
}
