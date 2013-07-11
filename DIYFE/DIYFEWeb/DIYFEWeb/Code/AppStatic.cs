using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using DIYFELib;
using DIYFE.EF;


namespace DIYFEWeb
{
    public class AppStatic
    {
        public static void LoadStaticCache()
        {
            //EXAMPLE OF HOW TO USE WITH A ORM TOOL
            //using (var context = new MDMContext())
            //{
            //    HttpContext.Current.Application["communicationType"] = context.CommunicationType.ToList();
            //}
            HttpContext.Current.Application["someVarName"] = "This is a test.  Normally a  list of objects but since no DB connection is made it's only a string...le sigh, poor string.";

            DIYFELib.ListAccess la = new DIYFELib.ListAccess();
            //List<Category> allCats = la.AllCategory();
            List<DIYFE.EF.Category> allCats = new List<DIYFE.EF.Category>();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                allCats = db.Categories.ToList();
            }
            HttpContext.Current.Application["Categories"] = allCats;

            List<DIYFE.EF.ContentSection> contentSections = new List<ContentSection>();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                contentSections = db.ContentSections.ToList();
            }
            HttpContext.Current.Application["ContentSections"] = contentSections;

            //BUILD TOP NAVIGATION ITEMS HTML
            #region

            string topNavString = "<ul role=\"navigation\" class=\"nav pull-right\">";
            foreach (Category firstCat in allCats.Where(c => c.SecondLevCategoryId == 0 && c.TopNavIndex > 0).OrderBy(c => c.TopNavIndex))
            {
              topNavString +=  "<li class=\"dropdown\" id=\"MainNav-Mfg\">";
              topNavString += "<a data-toggle=\"dropdown\" class=\"dropdown-toggle\" href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "\")>" + firstCat.CategoryName + "<b class=\"caret\"></b></a>";
              topNavString += "<ul class=\"dropdown-menu\">";
              foreach (Category secondCat in allCats.Where(c => c.CategoryId == firstCat.CategoryId && c.SecondLevCategoryId > 0 && c.ThirdLevCategoryId == 0))
              {
                  if (allCats.Where(c => c.CategoryId == firstCat.CategoryId && c.SecondLevCategoryId == secondCat.SecondLevCategoryId && c.ThirdLevCategoryId > 0).Count() > 0)
                  {
                      topNavString += "<li class=\"dropdown-submenu\">";
                      topNavString += "<a href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "/" + secondCat.SecondLevCategoryUrl + "\">" + secondCat.SecondLevCategoryName + "</a>";
                      topNavString += "<ul class=\"dropdown-menu\">";
                      //LOOP OVER THIRD LEVEL
                      foreach (Category thirdCat in allCats.Where(c => c.CategoryId == firstCat.CategoryId && c.SecondLevCategoryId == secondCat.SecondLevCategoryId && c.ThirdLevCategoryId > 0))
                      {
                          topNavString += "<li><a href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "/" + secondCat.SecondLevCategoryUrl + "/" + thirdCat.ThirdLevCategoryUrl + "\">" + thirdCat.ThirdLevCategoryName + "</a></li>";
                      }
                      
                      topNavString += "</ul></li>";
                  }
                  else
                  {
                      topNavString += "<li><a href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "/" + secondCat.SecondLevCategoryUrl + "\">" + secondCat.SecondLevCategoryName + "</a></li>";
                  }
              }
              topNavString += "</ul></li>";
            }
            topNavString += "</ul>";
            
            #endregion

            HtmlString hString = new HtmlString(topNavString);

            HttpContext.Current.Application["TopNavHtml"] = hString;
        }

        //EXAMPLE OF HOW TO USE WITH A ORM TOOL
        //public static List<CommunicationType> AllCommunicationTypes
        //{
        //    get
        //    {
        //        return HttpContext.Current.Application["communicationType"] as List<CommunicationType>;
        //    }
        //}

        public static bool isDebug
        {
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Debug"] as string == "true")
                { return true; }
                else
                { return false; }
            }
        }

        public static string SiteName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteName"] as string;
            }
        }

        public static string ApplicationVar
        {
            get
            {
                return HttpContext.Current.Application["someVarName"] as string;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return (List<Category>)(HttpContext.Current.Application["Categories"]);
            }
        }

        public static List<DIYFE.EF.ContentSection> ContentItems
        {
            get
            {
                return (List<DIYFE.EF.ContentSection>)(HttpContext.Current.Application["ContentSections"]);
            }
        }

        public static string LunceneIndexLocation
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LuceneIndexLocation"] as string;
            }
        }
        
        public static string BaseSiteUrl
        {
            get
            {
                //HttpContext context = HttpContext.Current;
                //string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/') + '/';
                //return baseUrl;
                return System.Configuration.ConfigurationManager.AppSettings["URL"] as string;
            }
        }

        #region Static Mail Settings

        public static string MailSenderAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SenderAddress"].ToString();
            }
        }

        public static string MailErrorAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ErrorAddress"].ToString();
            }
        }

        #endregion


        public static HtmlString TopNavHtml
        {
            get
            {
                return HttpContext.Current.Application["TopNavHtml"] as HtmlString;
            }
        }


        #region Static Mail Settings

        public static string EmailSenderAddress
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SenderAddress"].ToString();
            }
        }

        #endregion

    }
}