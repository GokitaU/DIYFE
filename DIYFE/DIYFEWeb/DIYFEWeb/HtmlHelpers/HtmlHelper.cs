using DIYFEWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DIYFEWebHelpers
{
    public static class HtmlHelpers
    {
        private const string Nbsp = "&nbsp;";

        public static HtmlString NbspIfEmpty(this HtmlHelper helper, string value)
        {
            return new HtmlString(string.IsNullOrEmpty(value) ? Nbsp : value);
        }

        public static MvcHtmlString ArticleLink(this HtmlHelper helper, DIYFE.EF.Article article)
        {
            //NOTE: THERE IS A BETTER WAY TO DO THIS...I'M BUSY...SORRY\
            //THE Exception GENERATED IS MOST LIKELY TO BE BECUASE THE Article didn't load it's Category
            //sooooo catch the exception and load it from cache
            try
            {
                if (article.Category == null)
                { }
            }
            catch (Exception ex)
            {
                article.Category = AppStatic.Categories.Where(c => c.CategoryRowId == article.CategoryRowId).FirstOrDefault();
            }

            //the article type is required to build the URL, not bothered by loading it or checking for it...could probably use a enum            
            string articleType = "";
            switch (article.ArticleTypeId)
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

            string ahref = "<a href=\"" + AppStatic.BaseSiteUrl + articleType + article.Category.CategoryUrl + "/";

            //MvcHtmlString ahref = new MvcHtmlString("<a href=\"" + AppStatic.BaseSiteUrl + article.Category.CategoryUrl + "/");
            if (!String.IsNullOrEmpty(article.Category.SecondLevCategoryUrl))
            {
                ahref += article.Category.SecondLevCategoryUrl + "/";
            }
            if (!String.IsNullOrEmpty(article.Category.ThirdLevCategoryUrl))
            {
                ahref += article.Category.ThirdLevCategoryUrl + "/";
            }
            ahref += article.URLLink + "\">" + article.Name + "</a>";
            // AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
            return new MvcHtmlString(ahref);
        }


        public static MvcHtmlString ArticleLink(this HtmlHelper helper, DIYFE.EF.Article article, string styleClass){
            //NOTE: THERE IS A BETTER WAY TO DO THIS...I'M BUSY...SORRY\
            //THE Exception GENERATED IS MOST LIKELY TO BE BECUASE THE Article didn't load it's Category
            //sooooo catch the exception and load it from cache
            try
            {
                if (article.Category == null)
                { }
            }
            catch (Exception ex)
            {
                article.Category = AppStatic.Categories.Where(c => c.CategoryRowId == article.CategoryRowId).FirstOrDefault();
            }

            //the article type is required to build the URL, not bothered by loading it or checking for it...could probably use a enum            
            string articleType = "";
            switch (article.ArticleTypeId) {
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

            string ahref = "<a class=\"" + styleClass + "\" href=\"" + AppStatic.BaseSiteUrl + articleType + article.Category.CategoryUrl + "/";

            //MvcHtmlString ahref = new MvcHtmlString("<a href=\"" + AppStatic.BaseSiteUrl + article.Category.CategoryUrl + "/");
            if (!String.IsNullOrEmpty(article.Category.SecondLevCategoryUrl))
            {
                ahref += article.Category.SecondLevCategoryUrl + "/";
            }
            if (!String.IsNullOrEmpty(article.Category.ThirdLevCategoryUrl))
            {
                ahref += article.Category.ThirdLevCategoryUrl + "/";
            }
            ahref += article.URLLink + "\">" + article.Name + "</a>";
            // AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
            return new MvcHtmlString(ahref);
        }

        public static MvcHtmlString ArticleLink(this HtmlHelper helper, DIYFE.EF.Article article, string styleClass, string linkText)
        {
            //NOTE: THERE IS A BETTER WAY TO DO THIS...I'M BUSY...SORRY
            try
            {
                if (article.Category == null)
                { }
            }
            catch (Exception ex)
            {
                using (var db = new DIYFE.EF.DIYFEEntities())
                {
                    article.Category = db.Categories.Where(c => c.CategoryRowId == article.CategoryRowId).FirstOrDefault();
                }
            }

            //TODO:the article type is required to build the URL, not bothered by loading it or checking for it...could probably use a enum            
            string articleType = "";
            switch (article.ArticleTypeId)
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

            string ahref = "<a class=\"" + styleClass + "\" href=\"" + AppStatic.BaseSiteUrl + articleType + article.Category.CategoryUrl + "/";

            //MvcHtmlString ahref = new MvcHtmlString("<a href=\"" + AppStatic.BaseSiteUrl + article.Category.CategoryUrl + "/");
            if (!String.IsNullOrEmpty(article.Category.SecondLevCategoryUrl))
            {
                ahref += article.Category.SecondLevCategoryUrl + "/";
            }
            if (!String.IsNullOrEmpty(article.Category.ThirdLevCategoryUrl))
            {
                ahref += article.Category.ThirdLevCategoryUrl + "/";
            }
            ahref += article.URLLink + "\">" + linkText + "</a>";
            // AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
            return new MvcHtmlString(ahref);
        }

        public static MvcHtmlString Link(this HtmlHelper helper, string url, string text, string title)
        {
            string link = "<a href=\"" + AppStatic.BaseSiteUrl + url + "\" title=\"" + title + "\">" + text + "</a>";
            return new MvcHtmlString(link);
        }

        public static MvcHtmlString Link(this HtmlHelper helper, string url, string text, string title, string styleClass)
        {
            string link = "<a class=\"" + styleClass + "\" href=\"" + AppStatic.BaseSiteUrl + url + "\" title=\"" + title + "\">" + text + "</a>";
            return new MvcHtmlString(link);
        }

    }
}