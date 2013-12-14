using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFE.EF;
using DIYFEWeb.Models;

namespace DIYFEWeb.Controllers
{

    public class ServiceController : Controller
    {
        //
        // GET: /Service/
        public ActionResult PostComment(ArticleComment comment)
        {
            var data = new object();

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    db.ArticleComments.Add(comment);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                data = new { success = false, message = "Failed to insert new comment." };
                return Json(data);
            }

             data = new { success = true, obj = comment };

            return Json(data);
        }


        //public ActionResult SendContactEmail(string firstName, string lastName, string email, string message)
        //{
        //    var data = new object();

        //    var emailBody = EmailMessageFactory.GetWebContact(
        //        firstName + " " + lastName, email, message);

        //    // Send the message
        //    var result = EmailClient.SendEmail(emailBody);

        //    // Check the result
        //    if (result.Failed)
        //    {
        //        data = new { success = false, message = "Failed to send email contact.  Please give us a call." };
        //        return Json(data);
        //    }
        //    else
        //    {
        //        data = new { success = true };
        //    }

        //    return Json(data);
        //}

  
        //public ActionResult JavascriptError(string errorMethod, string errorMessage)
        //{
        //    var data = new object();

        //    try
        //    {
        //        DIYFEWeb.Models.ErrorModel err = new DIYFEWeb.Models.ErrorModel();
        //        var ex = new Exception(errorMethod + " " + errorMessage);
        //        err.InsertError(ex, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        data = new { success = false, message = "Failed to insert javascript error." };
        //        return Json(data);
        //    }

        //        data = new { success = true };

        //    return Json(data);
        //}

    }
}
