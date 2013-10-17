using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;

namespace DIYFEWeb.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            ArticleListModel model = new ArticleListModel();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                // model.ProjectList = db.Articles.Include("ArticleStatus").OrderBy(a => a.UpdateDate).Take(5).ToList();
                //Projects needing funding
                model.ArticleList = db.Articles.Include("ArticleStatus").ToList();
                //model.ProjectList = db.Articles.Include("ArticleStatus").Where(a => a.ArticleStatus.Any(arts => arts.StatusId == 4)).Take(5).ToList();
                ////Projects recently updated
                //model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 2).OrderBy(a => a.UpdateDate).Take(5).ToList());
                ////Post recently added
                //model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 1).OrderBy(a => a.CreatedDate).Take(5).ToList());
                ////News recently added
                //model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 4).OrderBy(a => a.CreatedDate).Take(5).ToList());
                ////model.ProjectList = db.Articles.Include()
                ////var articles = db.Articles.Where(a => a.ArticleTypeId == 2 && a.ArticleStatus.Any(ar => ar.StatusId==3));

            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
