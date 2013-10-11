using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFELib;

namespace DIYFEWeb.Controllers
{

    public class ServiceController : Controller
    {
        //
        // GET: /Service/
        [LoggingFilter]
        public ActionResult PostComment(ArticleComment comment)
        {
            var data = new object();

            ArticleComment ac = new ArticleComment();
            if (ac.InsertComment(comment))
            {
                data = new { success = true, obj = comment };
            }
            else
            {
                data = new { success = false, message = "Failed to insert new comment." };
                return Json(data);
            }

            return Json(data);
        }

        [LoggingFilter]
        public ActionResult SendContactEmail(string firstName, string lastName, string email, string message)
        {
            var data = new object();

            var emailBody = EmailMessageFactory.GetWebContact(
                firstName + " " + lastName, email, message);

            // Send the message
            var result = EmailClient.SendEmail(emailBody);

            // Check the result
            if (result.Failed)
            {
                data = new { success = false, message = "Failed to send email contact.  Please give us a call." };
                return Json(data);
            }
            else
            {
                data = new { success = true };
            }

            return Json(data);
        }

        [LoggingFilter]
        public ActionResult JavascriptError(string errorMethod, string errorMessage)
        {
            var data = new object();

            try
            {
                DIYFEWeb.Models.ErrorModel err = new DIYFEWeb.Models.ErrorModel();
                var ex = new Exception(errorMethod + " " + errorMessage);
                err.InsertError(ex, 0);
            }
            catch (Exception ex)
            {
                data = new { success = false, message = "Failed to insert javascript error." };
                return Json(data);
            }

                data = new { success = true };

            return Json(data);
        }

    }
}
