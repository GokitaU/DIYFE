using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;

using DIYFEWeb.Models;
using DIYFE.EF;

namespace DIYFEWeb.Controllers
{
    public class ArticleController : ApplicationController
    {

        private int pageSize = 10;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ArticleList1(string prefix)
        {

            return View();
        }

        public ActionResult ArticleList(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, int? page)
        {
            ArticleListModel model = new ArticleListModel();
            var linkPrefix = HttpContext.Request.RawUrl.Split('/')[1];
            
            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            string url = HttpContext.Request.RawUrl;
            //int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");
            Category cat = StaticConfig.GetCatigory(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(cat, linkPrefix);
            model.RelatedTreeView = StaticConfig.GenerateRelatedTreeView(cat, linkPrefix);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //BASED ON CAT
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.Category.CategoryId == cat.CategoryId).OrderBy(a => a.CreatedDate);
                model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.Category.CategoryId == cat.CategoryId && a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);

                //CHECK PAGING
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 1);
                //model.PagedArticle = model.ArticleList.Concat(db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleTypeId == 2)).OrderBy(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
            }
            //model.PagedArticle = model.ArticleList.ToPagedList(page ?? 1, pageSize);
            return View(model);

            return View();
        }

        public ActionResult ArticleDetails(string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, string html)
        {
            int categoryRowId = 0;
           
            var linkPrefix = HttpContext.Request.RawUrl.Split('/')[1];

            ArticleModel model = new ArticleModel();
            Category cat = new Category();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Include("ArticleComments").Where(a => a.URLLink == html + ".html").FirstOrDefault();
                model.Article.ViewRequests++;
                db.Entry(model.Article).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            if (model.Article != null)
            {
                cat = StaticConfig.GetCatigroy(model.Article.CategoryRowId);
                
                model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(cat, linkPrefix);
                model.RelatedTreeView = StaticConfig.GenerateRelatedTreeView(cat, linkPrefix);

                PageModel.Title = model.Article.Title;
                PageModel.Description = model.Article.Description;
                PageModel.Author = model.Article.Author;
                PageModel.Keywords = model.Article.Keywords;
            }
            else
            {
                //JUST TO GET BY -- REPLACE WITH 404 SOULATION
                model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(new Category(), linkPrefix);
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
            model.RelatedTreeView = StaticConfig.GenerateTreeViewThirdLev(cat, linkPrefix);

            ////PageModel.Title = model.Article.Title;
            ////PageModel.Description = model.Article.Description;
            ////PageModel.Author = model.Article.Author;
            ////PageModel.Keywords = model.Article.Keywords;

            return View();
        }

    }
}
