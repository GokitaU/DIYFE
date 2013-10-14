using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DIYFE.EF;
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
                model.ProjectList = db.Articles.Include("ArticleStatus").Where(a => a.ArticleStatus.Any(arts => arts.StatusId == 4)).Take(5).ToList();
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

            int[] contentItemIds = { 7 };

            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems.Where(ci => contentItemIds.Contains(ci.ContentId)).ToList();

            return View(Model);
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
        public ActionResult Sponsorship()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            int[] contentItemIds = { 4,5,6,9 };

            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems.Where(ci => contentItemIds.Contains(ci.ContentId)).ToList();

            return View(Model);
        }

        [LoggingFilter]
        public ActionResult Goals()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            int[] contentItemIds = { 15 };

            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems.Where(ci => contentItemIds.Contains(ci.ContentId)).ToList();

            return View(Model);
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

            int[] contentItemIds = { 6 };

            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems.Where(ci => contentItemIds.Contains(ci.ContentId)).ToList();

            return View(Model);
        }

        //DON'T FORGET ABOUT THIS ONE!!!
        //THIS HOLDS INFORMATION ABOUT DONATINOS BUT MORE IMPORATNLY
        //IT HOLDS THE CONTACT INFORMATION ABOUT THE PROJECTS AND CAN CONTACT THE BUILDER TO HELP
        //THIS WOULD WORK REALLY WELL FOR LARGER PROJECT, AGRACULTER AND HOME BUILDING PROJECTS
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
        public ActionResult Projects()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            int[] contentItemIds = { 1,3,8,10,11,13,14 };
            
            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems.Where(ci => contentItemIds.Contains(ci.ContentId)).ToList();

            return View(Model);
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

        public ActionResult ContentText()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            //int[] contentItemIds = { 1, 2, 3, 8, 10, 11 };

            List<DIYFE.EF.ContentSection> Model = new List<DIYFE.EF.ContentSection>();
            Model = AppStatic.ContentItems;

            return View(Model);
        }

        public ActionResult WeeklyMenu()
        {
            PageModel.Title = "Contact About Boostrap Project";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavContact";

            WeeklyMenu Model = new WeeklyMenu();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                Model.ShoppingList = db.IngredientShoppings.ToList();
                Model.DailyRecipes = db.DailyRecipes.ToList();
                Model.SeasonalRecipes = db.SeasonalRecipes.ToList();
            }

            return View(Model);
        }

        public ActionResult DailyRecipeRemove(int recipeDayId)
        {

            var data = new object();

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    RecipeDay deltedRecipe = db.RecipeDays.Where(rd => rd.RecipeDayId == recipeDayId).FirstOrDefault();
                    if (deltedRecipe != null)
                    {
                        db.Entry(deltedRecipe).State = System.Data.EntityState.Deleted;
                        db.SaveChanges();
                    };
                }

                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

        public ActionResult AddToDay(int dayId, int recipeId)
        {

            var data = new object();

            RecipeDay rd = new RecipeDay
            {
                DayId = dayId,
                RecipeId = recipeId,
                Active = true
            };

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {

                    db.Entry(rd).State = System.Data.EntityState.Added;
                    db.SaveChanges();
                }

                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

        public ActionResult AddShoppingItem(int ingredientId)
        {

            var data = new object();

            IngredientShopping rd = new IngredientShopping
            {
                IngredientId = ingredientId
            };

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {

                    db.Entry(rd).State = System.Data.EntityState.Added;
                    db.SaveChanges();
                }

                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

        public ActionResult RemoveShoppingItem(int ingredientId)
        {

            var data = new object();

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {

                    IngredientShopping deltedItem = db.IngredientShoppings.Where(i => i.IngredientId == ingredientId).FirstOrDefault();
                    if (deltedItem != null)
                    {
                        db.Entry(deltedItem).State = System.Data.EntityState.Deleted;
                        db.SaveChanges();
                    };
                }

                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

        public ActionResult ClearWeek()
        {

            var data = new object();

            try
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    db.ClearWeek();
                    db.SaveChanges();
                }

                data = new { success = true };
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    data = new { success = false, message = ex.InnerException.Message };
                }
                else
                {
                    data = new { success = false, message = ex.Message + " Another reason why EF sucks" };
                }
                return Json(data);

            }

            return Json(data);
        }

    }
}
