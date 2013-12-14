using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList;

using DIYFEWeb.Models;
using DIYFE.EF;

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
            Article content = new Article()
            {
                Name="Contact",
                MainContent = "<p>Sure. You'd be surprised how far a hug goes with Geordi, or Worf. Talk about going nowhere fast. This is not about revenge. This is about justice. Computer, lights up! Our neural pathways have become accustomed to your sensory input patterns. Some days you get the bear, and some days the bear gets you. Maybe if we felt any human loss as keenly as we feel one of those close to us, human history would be far less bloody.</p>",
                URLLink = ""
            };

            return View(content);
        }

        public ActionResult Product_Promotion()
        {
            Article content = new Article()
            {
                Name = "Product and Project Promotion",
                MainContent = "<p>Wait a minute - you've been declared dead. You can't give orders around here. Congratulations - you just destroyed the Enterprise. I can't. As much as I care about you, my first duty is to the ship. Smooth as an android's bottom, eh, Data? Well, that's certainly good to know. I suggest you drop it, Mr. Data. But the probability of making a six is no greater than that of rolling a seven. A surprise party? Mr. Worf, I hate surprise parties. I would *never* do that to you.</p>",
                URLLink = "promo"
            };

            return View("Contact", content);
        }

        public ActionResult Ad_Space()
        {
            Article content = new Article()
            {
                Name = "Get Noticed With Ad Space",
                MainContent = "<p>Wait a minute - you've been declared dead. You can't give orders around here. Congratulations - you just destroyed the Enterprise. I can't. As much as I care about you, my first duty is to the ship. Smooth as an android's bottom, eh, Data? Well, that's certainly good to know. I suggest you drop it, Mr. Data. But the probability of making a six is no greater than that of rolling a seven. A surprise party? Mr. Worf, I hate surprise parties. I would *never* do that to you.</p>",
                URLLink = "adSpace"
            };

            return View("Contact", content);

        }

        public ActionResult SignUpNewLetter(string email)
        {
            var data = new object();

            try
            {
                NewsLetter letter = new NewsLetter()
                {
                    Email = email,
                    DateCreated = DateTime.Now
                };
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    db.NewsLetters.Add(letter);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                data = new { success = false, message = "Failed to join news letter." };
                return Json(data);
            }
            

            data = new { success = true };

            return Json(data);
        }

        public ActionResult Connect()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Kickstarter()
        {
            return View();
        }

        public ActionResult SendContactEmail(ContactMailModel model)
        {
            var data = new object();
            
            try
            {
                var email = EmailMessageFactory.GetContactEmail(model);
                var result = EmailClient.SendEmail(email);

                if (!String.IsNullOrEmpty(model.NewsLetter))
                {
                    try
                    {
                        NewsLetter letter = new NewsLetter()
                        {
                            Email = model.Email,
                            DateCreated = DateTime.Now
                        };
                        using (var db = new DIYFE.EF.DIYFEEntities())
                        {
                            db.NewsLetters.Add(letter);
                            db.SaveChanges();
                        }

                    }
                    catch (Exception ex)
                    {
                        data = new { success = false, message = "Message Was Sent...but failed to join news letter." };
                        return Json(data);
                    }
                }
            }
            catch (Exception ex)
            {
                data = new { success = false, message = "Failed to send comment.  Please trying contacting us directly." };
                return Json(data);
            }

            data = new { success = true };

            return Json(data);

        }

        public ActionResult Search(string searchVal, int? page)
        {
            ArticleListModel model = new ArticleListModel();

            PageModel.Title = "";
            PageModel.Description = searchVal;
            PageModel.Author = "";
            PageModel.Keywords = "";

            //            string url = HttpContext.Request.RawUrl;
            //int categoryRowId = DIYFEHelper.GetCatigoryRowId(categoryUrl, "", "");

            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                //BASED ON CAT
                //mo
                model.PagedArticle = db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleType.ArticleTypeName == "post" && a.ArticleStatus.Any(aStat => aStat.StatusId == 1)).OrderByDescending(a => a.CreatedDate).ToPagedList(page ?? 1, 10);
                //CHECK PAGING
                //model.ArticleList = db.Articles.Include("ArticleComments").Where(a => a.ArticleTypeId == 1);
                //model.PagedArticle = model.ArticleList.Concat(db.Articles.Include("ArticleComments").Include("ArticleStatus.StatusType").Where(a => a.ArticleTypeId == 2)).OrderBy(a => a.CreatedDate).ToPagedList(page ?? 1, pageSize);
            }
            //model.PagedArticle = model.ArticleList.ToPagedList(page ?? 1, pageSize);
            return View(model);
        }

    }
}
