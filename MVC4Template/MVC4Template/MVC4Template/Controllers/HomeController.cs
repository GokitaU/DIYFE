using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4Template.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {

            PageModel.Title = "Page Title";
            PageModel.Description = "Page Description";
            PageModel.Author = "Page Author";
            PageModel.Keywords = "Page Keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";


            //var errMess = new Exception("The main message", new Exception("the inner message"));

            //MVC4Template.Models.ErrorModel err = new MVC4Template.Models.ErrorModel();
            //err.InsertError(errMess);

            #region Send Email


           ////  Create the mail object
           // var email = EmailMessageFactory.GetWelcomeEmail(
           //     "sumdood31@gmail.com",
           //     "jdoe123",
           //     "John Doe",
           //     "nerdyp@ss",
           //     "http://www.loginurl");

           // // Send the message
           // var result = EmailClient.SendEmail(email);

           // // Check the result
           // if (result.Failed)
           // {
           //     //show error
           // }

            #endregion

            return View();
        }

        public ActionResult About()
        {
            PageModel.Title = "Page Title";
            PageModel.Description = "Page Description";
            PageModel.Author = "Page Author";
            PageModel.Keywords = "Page Keywords";
            PageModel.ActiveTopNavLink = "MainNavAbout";

            return View();
        }

        public ActionResult Contact()
        {
            PageModel.Title = "Page Title";
            PageModel.Description = "Page Description";
            PageModel.Author = "Page Author";
            PageModel.Keywords = "Page Keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            return View();
        }
    }
}
