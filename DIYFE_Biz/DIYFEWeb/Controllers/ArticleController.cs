﻿using System;
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

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult ArticleTest(string articleType, string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, string articleName)
        //{

        //    return View();
        //}

        //public ActionResult ArticleList1(string articleType, string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, int? page)
        //{

        //    return View();
        //}

        public ActionResult ArticleListRoot(string articleType, int? page)
        {

            ArticleListModel model = new ArticleListModel();

            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

//            string url = HttpContext.Request.RawUrl;
            //int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //BASED ON CAT
                //mo
                    model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleType.ArticleTypeName == articleType && a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
                
                //CHECK PAGING
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 1);
                //model.PagedArticle = model.ArticleList.Concat(db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleTypeId == 2)).OrderBy(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
            }
            //model.PagedArticle = model.ArticleList.ToPagedList(page ?? 1, pageSize);
            return View(model);
        }

        public ActionResult ArticleList(string articleType, string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, int? page)
        {
           
            ArticleListModel model = new ArticleListModel();
            
            PageModel.Title = "";
            PageModel.Description = "";
            PageModel.Author = "";
            PageModel.Keywords = "";

            string url = HttpContext.Request.RawUrl;
            //int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");
     
            Category cat = StaticConfig.GetCatigory(categoryUrl, subCategoryUrl, subSubCategoryUrl);

            model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(cat, articleType);
            model.RelatedTreeView = StaticConfig.GenerateRelatedTreeView(cat, articleType);
            model.PageLinkBase = StaticConfig.BaseSiteUrl + articleType;
            if (categoryUrl != ""){ model.PageLinkBase += "/" + cat.CategoryUrl; }
            if (subCategoryUrl != "") { model.PageLinkBase += "/" + cat.SecondLevCategoryUrl; }
            if (subSubCategoryUrl != "") { model.PageLinkBase += "/" + cat.ThirdLevCategoryUrl; }
            //model.PageLinkBase = model.CrumbLinkList.Last().Href;
            //model.RelatedTreeView = StaticConfig.TreeView(cat, model.PageLinkBase);

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                
                //BASED ON CAT
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.Category.CategoryId == cat.CategoryId).OrderBy(a => a.CreatedDate);
                if (categoryUrl != "")
                {
                    model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.Category.CategoryId == cat.CategoryId && a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
                    //model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
                    if (subSubCategoryUrl != "")
                    { model.Into = db.Articles.Where(a => a.URLLink == subSubCategoryUrl).FirstOrDefault(); }
                    else if (subCategoryUrl != "")
                    { model.Into = db.Articles.Where(a => a.URLLink == subCategoryUrl).FirstOrDefault(); }
                    else
                    { model.Into = db.Articles.Where(a => a.URLLink == categoryUrl).FirstOrDefault(); }
                }
                else
                {
                    model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleType.ArticleTypeName == articleType && a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
                }

                model.Into = new Article();
                model.Into.Title = "Text of H1 in work";
                //CHECK PAGING
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 1);
                //model.PagedArticle = model.ArticleList.Concat(db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleTypeId == 2)).OrderBy(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
            }
            //model.PagedArticle = model.ArticleList.ToPagedList(page ?? 1, pageSize);
            return View(model);
        }

        public ActionResult ArticleDetails(string articleType, string categoryUrl, string subCategoryUrl, string subSubCategoryUrl, string articleName)
        {
            int categoryRowId = 0;
           
            ArticleModel model = new ArticleModel();
            Category cat = new Category();
            
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                model.Article = db.Articles.Include("ArticleComments").Where(a => a.URLLink == articleName + ".html").FirstOrDefault();
                model.Article.ViewRequests++;
                db.Entry(model.Article).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            if (model.Article != null)
            {
                cat = StaticConfig.GetCatigroy(model.Article.CategoryRowId);

                model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(cat, articleType);
                model.RelatedTreeView = StaticConfig.GenerateRelatedTreeView(cat, articleType);

                PageModel.Title = model.Article.Title;
                PageModel.Description = model.Article.Description;
                PageModel.Author = model.Article.Author;
                PageModel.Keywords = model.Article.Keywords;
            }
            else
            {
                //JUST TO GET BY -- REPLACE WITH 404 SOULATION
                model.CrumbLinkList = StaticConfig.GenerateCrumbLinks(new Category(), articleType);
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
            model.RelatedTreeView = StaticConfig.GenerateTreeViewThirdLev(cat, articleType);

            ////PageModel.Title = model.Article.Title;
            ////PageModel.Description = model.Article.Description;
            ////PageModel.Author = model.Article.Author;
            ////PageModel.Keywords = model.Article.Keywords;

            return View(model);
        }

    }
}
