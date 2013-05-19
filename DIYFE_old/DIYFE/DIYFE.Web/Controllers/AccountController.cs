using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DIYFE.Web.Models;

namespace DIYFE.Web.Controllers
{
    public class AccountController : ApplicationController
    {

        //
        // GET: /Account/LogOn
        [LoggingFilter]
        public ActionResult LogOn()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View();
        }

        [LoggingFilter]
        public ActionResult LogOff()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";


            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [LoggingFilter]
        public ActionResult Register()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View();
        }

        [Authorize]
        [LoggingFilter]
        public ActionResult ChangePassword()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View();
        }

        [LoggingFilter]
        public ActionResult ChangePasswordSuccess()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View();
        }

        #region HTTP POST

        [LoggingFilter]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            var data = new object();
            try
            {
                if (ModelState.IsValid)
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        if (Url.IsLocalUrl(model.ReturnURL) && model.ReturnURL.Length > 1 && model.ReturnURL.StartsWith("/")
                            && !model.ReturnURL.StartsWith("//") && !model.ReturnURL.StartsWith("/\\"))
                        {
                            return Redirect(model.ReturnURL);
                        }
                        else
                        {
                            data = new { success = true };
                            //return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        data = new { success = false, message = "The user name or password provided is incorrect." };
                    }
                }
                else
                {
                    data = new { success = false, message = "Please correct any form errors and try again." };
                }
            }
            catch (Exception ex)
            {
                data = new { success = false, message = ex.InnerException };
            }


            // If we got this far, something failed, redisplay form
            return Json(data);
        }

        [LoggingFilter]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [LoggingFilter]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
