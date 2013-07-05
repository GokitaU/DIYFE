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
        
        [LoggingFilter]
        public ActionResult Index()
        {
            ArticleListModel model = new ArticleListModel();
            model.ProjectList = new List<DIYFE.EF.Article>();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
               // model.ProjectList = db.Articles.Include("ArticleStatus").OrderBy(a => a.UpdateDate).Take(5).ToList();
                //Projects needing funding
                model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleStatus.Any(arts => arts.StatusId == 4)).Take(5).ToList());
                //Projects recently updated
                model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 2).OrderBy(a => a.UpdateDate).Take(5).ToList());
                //Post recently added
                model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 1).OrderBy(a => a.CreatedDate).Take(5).ToList());
                //News recently added
                model.ProjectList.AddRange(db.Articles.Include("ArticleStatus").Where(a => a.ArticleTypeId == 4).OrderBy(a => a.CreatedDate).Take(5).ToList());
                //model.ProjectList = db.Articles.Include()
                //var articles = db.Articles.Where(a => a.ArticleTypeId == 2 && a.ArticleStatus.Any(ar => ar.StatusId==3));
            }
            model.ProjectList = model.ProjectList.Distinct().ToList();
            PageModel.Title = "DiyFe";
            PageModel.Description = "";
            PageModel.Author = "Do it yourself for everyone.";
            PageModel.Keywords = "DIY, DIYFE, do it yourself, homesteading, transition";
            //PageModel.ActiveTopNavLink = "MainNavHome";

            //Test error handeling
            //throw new Exception();

            #region Check Valid Domain and Email

            //EmailValidation.Validate validate = new EmailValidation.Validate();
            //EmailValidation.DnsMx
            //string[] domainNames = new string[] { "hotmail.com", "", "none.com", "tetsicala.ca" };
            //foreach (string dName in domainNames)
            //{
            //    if (validate.Domain(dName))
            //    {
            //        var test = true;
            //    }
            //}

            //string[] emailNames = new string[] { "mx1.hotmail.com" };
            //foreach (string eName in emailNames)
            //{
            //    if (validate.Email(eName))
            //    {
            //        var test = true;
            //    }
            //}


            //string[] s = EmailValidation.DnsMx.GetMXRecords("hotmail.com");
            //foreach (string cm in s)
            //{
            //    var temp = cm;
            //}

            #endregion

            #region Send Email
            // Get the settings from the App.Config file
            //var loginUrl = "WebSiteURL";

           //  Create the mail object
            //var email = EmailMessageFactory.GetWelcomeEmail(
            //    "jdoe@example.com",
            //    "jdoe123",
            //    "John Doe",
            //    "nerdyp@ss",
            //    loginUrl,
            //    AppStatic.EmailSenderAddress);

            //// Send the message
            //var result = EmailClient.SendEmail(email);

            //// Check the result
            //if (result.Failed)
            //{
            //    Console.WriteLine(result.Exception.Message);
            //}
            //else
            //{
            //    Console.WriteLine("Sent mail:");
            //    Console.Write(result.Message.Body);
            //}
            #endregion

            string testStaticAppVar = AppStatic.ApplicationVar;

            return View(model);
        }

        [LoggingFilter]
        public ActionResult About()
        {
            PageModel.Title = "About DiyFe";
            PageModel.Description = "";
            PageModel.Author = "Do it yourself for everyone.";
            PageModel.Keywords = "DIY, DIYFE, do it yourself, homesteading, transition";
            PageModel.ActiveTopNavLink = "MainNavAbout";

            return View();
        }

        [LoggingFilter]
        public ActionResult Contact()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }

        [LoggingFilter]
        public ActionResult Who()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }

        [LoggingFilter]
        public ActionResult Goals()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }

        [LoggingFilter]
        public ActionResult Donations()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }

        [LoggingFilter]
        public ActionResult GettingSponsored()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }

        [LoggingFilter]
        public ActionResult Participate()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }


        [LoggingFilter]
        public ActionResult Search(string term)
        {

            PageModel.Title = "Search Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavSearch";

            if (term != null)
            {
                //var data = new SearchIndex.LuceneIndex().MemberSearch(term);
                List<SearchIndex.LuceneData> result = new SearchIndex.LuceneIndex().MemberSearch(term);


                return View(result);
            }

            return View();
        }

    }
}
