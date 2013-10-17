using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFE.EF;

namespace DIYFEWeb
{
    public class StaticConfig
    {
        public static void LoadStaticCache()
        {
          
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
            string topNav = "<div id=\"header\"><nav id=\"nav\"><div class=\"nav-holder\"><div class=\"nav-frame\"><ul class=\"nav\" style=\"display: block;\">";
            string subNav = "<div class=\"subnav\">";
            int topNavIndex = 0;
            foreach (Category firstCat in allCats.Where(c => c.SecondLevCategoryId == 0 && c.TopNavIndex > 0).OrderBy(c => c.TopNavIndex))
            {
                topNav += "<li><a rel=\"sub" + topNavIndex.ToString() + "\" href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "\"><div><span>" + firstCat.CategoryName + "</span></div></a></li>";
                subNav += "<ul id=\"sub" + topNavIndex.ToString() + "\" class=\"submenu\" style=\"display: none;\">";
                foreach (Category secondCat in allCats.Where(c => c.CategoryId == firstCat.CategoryId && c.SecondLevCategoryId > 0 && c.ThirdLevCategoryId == 0).OrderBy(c => c.SubNavIndex))
                {
                    subNav += "<li><a href=\"" + BaseSiteUrl + "post/" + firstCat.CategoryUrl + "/" + secondCat.SecondLevCategoryUrl + "\">" + secondCat.SecondLevCategoryName + "</a></li>";
                }
                subNav += "</ul>";
                topNavIndex++;
            }
            topNav += "</ul></div></nav></div>";
            subNav += "</div>";

            HtmlString hString = new HtmlString(topNav + subNav);


            #endregion



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
        public static string BaseSiteUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["URL"] as string;
            }
        }

        public static string SiteName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteName"] as string;
            }
        }

        public static List<Category> Categories
        {
            get
            {
                return (List<Category>)(HttpContext.Current.Application["Categories"]);
            }
        }

        public static string LunceneIndexLocation
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LuceneIndexLocation"] as string;
            }
        }
        
        public static string WebConfigSettingTest
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TestAppSetting"].ToString();
            }
        }

        public static HtmlString TopNavHtml
        {
            get
            {
                return HttpContext.Current.Application["TopNavHtml"] as HtmlString;
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

    }
}