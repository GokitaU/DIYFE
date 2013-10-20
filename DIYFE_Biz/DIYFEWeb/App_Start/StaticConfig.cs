using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFEWeb.Models;
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

        public static List<CustomHtmlLink> GenerateCrumbLinks(DIYFE.EF.Category cat, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();
            CustomHtmlLink rootLink = new CustomHtmlLink();
            rootLink.LinkText = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(linkPrefix) + "s";
            rootLink.Href = StaticConfig.BaseSiteUrl + linkPrefix + "/";
            rootLink.Title = linkPrefix;
            linkList.Add(rootLink);

            if (cat != null)
            {
                if (!String.IsNullOrEmpty(cat.CategoryUrl))
                {
                    CustomHtmlLink link = new CustomHtmlLink();
                    link.LinkText = cat.CategoryName;
                    link.Href = StaticConfig.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl;
                    link.Title = cat.CategoryName;
                    linkList.Add(link);
                }
                if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
                {
                    CustomHtmlLink link = new CustomHtmlLink();
                    link.LinkText = cat.SecondLevCategoryName;
                    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl;
                    link.Title = cat.SecondLevCategoryName;
                    linkList.Add(link);
                }
                if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
                {
                    CustomHtmlLink link = new CustomHtmlLink();
                    link.LinkText = cat.ThirdLevCategoryName;
                    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl + "/" + cat.ThirdLevCategoryUrl;
                    link.Title = cat.ThirdLevCategoryName;
                    linkList.Add(link);
                }
            }

            return linkList;
        }

        public static List<CustomHtmlLink> GenerateRelatedTreeView(Category cat, string linkPrefix)
        {

            if (cat == null)
            {
                cat = StaticConfig.Categories.FirstOrDefault();
            }

            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //ListAccess la = new ListAccess();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();
            linkList.Add(new CustomHtmlLink
            {
                LinkText = cat.CategoryName,
                Href = StaticConfig.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
                Title = cat.CategoryName,
                SubLinks = GenerateTreeViewSecondLev(cat, linkPrefix)
            });

            linkList[0].SubLinks.AddRange(RelatedArticleLinks(cat, linkPrefix, 1));

            return linkList;
        }

        public static List<CustomHtmlLink> GenerateTreeViewSecondLev(Category cat, string linkPrefix)
        {

            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();

            List<Category> secondLevCats = StaticConfig.Categories
                                                .Where(c => c.CategoryId == cat.CategoryId
                                                && c.SecondLevCategoryId > 0
                                                && c.ThirdLevCategoryId == 0)
                                                .ToList();

            foreach (Category _secondLevCat in secondLevCats)
            {
                CustomHtmlLink newCat = new CustomHtmlLink
                {
                    LinkText = _secondLevCat.SecondLevCategoryName,
                    Href = StaticConfig.BaseSiteUrl + linkPrefix + "/" + _secondLevCat.CategoryUrl + "/" + _secondLevCat.SecondLevCategoryUrl,
                    Title = _secondLevCat.SecondLevCategoryName,
                    SubLinks = GenerateTreeViewThirdLev(_secondLevCat, linkPrefix)
                };
                newCat.SubLinks.AddRange(RelatedArticleLinks(_secondLevCat, StaticConfig.BaseSiteUrl + linkPrefix, 2));

                linkList.Add(newCat);

            }

            return linkList;
        }

        public static List<CustomHtmlLink> GenerateTreeViewThirdLev(Category cat, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //ListAccess la = new ListAccess();

            List<Category> thirdLevCats = StaticConfig.Categories
                                                .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
                                                && c.ThirdLevCategoryId > 0)
                                                .ToList();
            //List<Category> thirdLevelCats = AppStatic.Categories
            //                                    .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
            //                                     && !String.IsNullOrEmpty(c.ThirdLevCategoryName))
            //                                    .ToList();

            foreach (Category _thirdLevCat in thirdLevCats)
            {
                CustomHtmlLink newCat = new CustomHtmlLink
                {
                    LinkText = _thirdLevCat.ThirdLevCategoryName,
                    Href = StaticConfig.BaseSiteUrl + linkPrefix + "/" + _thirdLevCat.CategoryUrl + "/" + _thirdLevCat.SecondLevCategoryUrl + "/" + _thirdLevCat.ThirdLevCategoryUrl,
                    Title = _thirdLevCat.ThirdLevCategoryName,
                    SubLinks = RelatedArticleLinks(_thirdLevCat, StaticConfig.BaseSiteUrl + linkPrefix, 3)
                };
                newCat.SubLinks.AddRange(RelatedArticleLinks(_thirdLevCat, StaticConfig.BaseSiteUrl + linkPrefix, 2));

                linkList.Add(newCat);
                //newCat.SubLinks.AddRange(GenerateTreeViewThirdLev(_secondLevCat.SecondLevCategoryId, linkPrefix));
            }

            return linkList;
        }

        public static List<CustomHtmlLink> RelatedArticleLinks(Category cat, string linkPrefix, int categoryLevel)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            List<Article> articles = new List<Article>();
            using (var db = new DIYFE.EF.DIYFEEntities())
            {
                switch (categoryLevel)
                {
                    case 1:
                        articles = db.Articles.Where(a => a.Category.CategoryId == cat.CategoryId
                            && a.Category.SecondLevCategoryId == 0
                            && a.Category.ThirdLevCategoryId == 0
                            ).ToList();
                        // whereSql = "WHERE CategoryId = " + cat.CategoryId;
                        break;
                    case 2:
                        articles = db.Articles.Where(a => a.Category.SecondLevCategoryId == cat.SecondLevCategoryId
                            && a.Category.ThirdLevCategoryId == 0
                            ).ToList();
                        //whereSql = "WHERE SecondLevCategoryId = " + cat.SecondLevCategoryId + " AND CategoryId= " + cat.CategoryId + " AND ThirdLevCategoryId = 0";
                        break;
                    case 3:
                        articles = db.Articles.Where(a => a.Category.ThirdLevCategoryId == cat.ThirdLevCategoryId).ToList();
                        // whereSql = "WHERE ThirdLevCategoryId = " + cat.ThirdLevCategoryId;
                        break;
                    case 4:
                        articles = db.Articles.Where(a => a.Category.CategoryId == cat.CategoryId).ToList();
                        //whereSql = "WHERE CategoryId = " + cat.CategoryId;
                        break;
                    default:
                        break;
                }
            }

            foreach (Article a in articles)
            {

                string articleType = "";
                switch (a.ArticleTypeId)
                {
                    case 1:
                        articleType = "post/";
                        break;
                    case 2:
                        articleType = "project/";
                        break;
                    case 3:
                        articleType = "blog/";
                        break;
                    case 4:
                        articleType = "news/";
                        break;
                    default:
                        articleType = "home/";
                        break;
                }

                string ahref = StaticConfig.BaseSiteUrl + articleType + cat.CategoryUrl + "/";
                //MvcHtmlString ahref = new MvcHtmlString("<a href=\"" + AppStatic.BaseSiteUrl + article.Category.CategoryUrl + "/");
                if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
                {
                    ahref += cat.SecondLevCategoryUrl + "/";
                }
                if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
                {
                    ahref += cat.ThirdLevCategoryUrl + "/";
                }
                ahref += a.URLLink;

                CustomHtmlLink htmlLink = new CustomHtmlLink
                {
                    LinkText = a.Name,
                    Href = ahref,
                    Title = a.Title
                };
                linkList.Add(htmlLink);
            }

            return linkList;

        }

        public static Category GetCatigory(string catOne, string catTwo, string catThree)
        {
            
            Category cat = new Category();
            catOne = catOne.ToLower();
            catTwo = catTwo.ToLower();
            catThree = catThree.ToLower();
            if (catOne.Length > 0 | catTwo.Length > 0 | catThree.Length > 0)
            {
                if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
                {
                    cat = StaticConfig.Categories
                                    .Where(c => c.CategoryUrl == catOne
                                            && c.SecondLevCategoryUrl == catTwo
                                            && c.ThirdLevCategoryUrl == catThree)
                                    .FirstOrDefault();
                }
                else if (catOne.Length > 0 && catTwo.Length > 0)
                {
                    cat = StaticConfig.Categories
                                    .Where(c => c.CategoryUrl == catOne
                                            && c.SecondLevCategoryUrl == catTwo
                                            && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
                                    .FirstOrDefault();
                }
                else
                {
                    cat = StaticConfig.Categories
                                    .Where(c => c.CategoryUrl == catOne
                                            && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
                                    .FirstOrDefault();
                }
            }
            else
            {
                cat = null;
            }

            return cat;
        }

        public static Category GetCatigroy(int categoryRowId)
        {
            Category cat = new Category();
            cat = StaticConfig.Categories
                            .Where(c => c.CategoryRowId == categoryRowId)
                            .FirstOrDefault();
            return cat;
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