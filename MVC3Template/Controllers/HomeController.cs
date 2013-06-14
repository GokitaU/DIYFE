using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC3Template.Models;

namespace MVC3Template.Controllers
{
    public class HomeController : ApplicationController
    {
        
        [LoggingFilter]        
        public ActionResult Index()
        {
            PageModel.Title = "Bootstrap Home Page";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

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

            // Create the mail object
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
            string testStaticWebConfigAppVar = AppStatic.WebConfigSettingTest;

            return View();
        }

        [LoggingFilter] 
        public ActionResult About()
        {
            PageModel.Title = "About the Bootstrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
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
        public ActionResult Search(string term)
        {
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
