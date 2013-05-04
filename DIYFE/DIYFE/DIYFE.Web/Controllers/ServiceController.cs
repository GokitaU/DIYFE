using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFELib;

namespace DIYFE.Web.Controllers
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
                data = new { success = true };
            }
            else
            {
                data = new { success = false, message = "Failed to insert new comment." };
                return Json(data);
            }

            return Json(data);
        }

    }
}
