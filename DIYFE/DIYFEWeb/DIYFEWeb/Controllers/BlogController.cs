using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFEWeb.Models;

namespace DIYFEWeb.Controllers
{
    public class BlogController : ApplicationController
    {
        //
        // GET: /Blog/

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
                model.ArticleList = db.Articles.Where(a => a.ArticleTypeId == 3).OrderBy(a => a.CreatedDate).ToList();
            }

            //model.ArticleList = la.ArticleList(catigoryId, 1);

            return View(model);
        }

    }
}
