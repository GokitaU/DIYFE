using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFEWeb.Models;
using DIYFE.EF;
using System.Data.SqlClient;


namespace DIYFEWeb.Code
{
    public static class DIYFEHelper
    {
        //public static List<CustomHtmlLink> GenerateCrumbLinks(string Url)
        //{
        //    List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            
        //    return linkList;
        //}

        public static List<CustomHtmlLink> GenerateCrumbLinks(DIYFE.EF.Category cat, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();
            CustomHtmlLink rootLink = new CustomHtmlLink();
            rootLink.LinkText = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(linkPrefix) + "s";
            rootLink.Href = AppStatic.BaseSiteUrl + linkPrefix + "/";
            rootLink.Title = "Base diyfe categor";
            linkList.Add(rootLink);

            if (cat != null)
            {
                if (!String.IsNullOrEmpty(cat.CategoryUrl))
                {
                    CustomHtmlLink link = new CustomHtmlLink();
                    link.LinkText = cat.CategoryName;
                    link.Href = AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl;
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
            else
            {
                throw (new Exception("Unable to find correct category."));
            }

            return linkList;
        }


        /// <summary>
        /// TODO: REVIEW THIS CRAP, COULD TOTALY DO BETTER
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="linkPrefix"></param>
        /// <returns></returns>
        public static List<CustomHtmlLink> GenerateRelatedTreeView(Category cat, string linkPrefix)
        {
            
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //ListAccess la = new ListAccess();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();
            linkList.Add(new CustomHtmlLink
               {
                    LinkText = cat.CategoryName,
                    Href = AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
                    Title = cat.CategoryName,
                    SubLinks = RelatedArticleLinks(cat, linkPrefix, 1)
                });

            //if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
            //{
            //    CustomHtmlLink htmlLink = new CustomHtmlLink
            //    {
            //        LinkText = cat.ThirdLevCategoryName,
            //        Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl + "/" + cat.ThirdLevCategoryUrl,
            //        Title = cat.ThirdLevCategoryName,
            //        SubLinks = la.RelatedArticleLinks(cat, linkPrefix, 3)
            //    };
            //    //linkList[0].SubLinks=la.RelatedArticleLinks(cat, linkPrefix, 3);
                
            //    List<Category> secondLevelCats = AppStatic.Categories
            //                                    .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
            //                                    && c.CategoryRowId != categoryRowId).ToList();
            //    foreach (Category _cat in secondLevelCats)
            //    {
            //        //linkList.Add(new CustomHtmlLink
            //        //{
            //        //    LinkText = _cat.SecondLevCategoryName,
            //        //    Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + _cat.SecondLevCategoryUrl,
            //        //    Title = _cat.SecondLevCategoryName + "-category",
            //        //});
                    
            //        //(!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
            //        //{

            //        //}

            //        //linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix);
            //    }
            //    return linkList;
            //}

            //if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
            //{
            //    List<Category> secondLevelCats = AppStatic.Categories
            //                                    .Where(c => c.CategoryId == cat.CategoryId).ToList();
            //    foreach (Category _cat in secondLevelCats)
            //    {
            //        linkList.Add(new CustomHtmlLink
            //        {
            //            LinkText = _cat.CategoryName,
            //            Href = "/" + linkPrefix + "/" + _cat.CategoryUrl,
            //            Title = _cat.CategoryName
            //        });
            //        if (!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
            //        {
            //            linkList.Last().SubLinks = GenerateRelatedTreeView(_cat.CategoryRowId, linkPrefix);
            //        }
            //        else
            //        {
            //            linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 3);
            //        }
            //    }
            //    return linkList;
            //}

            //ListAccess la = new ListAccess();
            //linkList = la.RelatedLinks(cat, linkPrefix);

            return linkList;
        }

        public static List<CustomHtmlLink> GenerateTreeViewSecondLev(Category cat, string linkPrefix)
        {

            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //ListAccess la = new ListAccess();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();

            List<Category> secondLevCats = AppStatic.Categories
                                                .Where(c => c.CategoryId == cat.CategoryId
                                                && c.SecondLevCategoryId > 0
                                                && c.ThirdLevCategoryId == 0)
                                                .ToList();
            //List<Category> thirdLevelCats = AppStatic.Categories
            //                                    .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
            //                                     && !String.IsNullOrEmpty(c.ThirdLevCategoryName))
            //                                    .ToList();

            foreach (Category _secondLevCat in secondLevCats)
            {
                CustomHtmlLink newCat = new CustomHtmlLink
                        {
                            LinkText = _secondLevCat.SecondLevCategoryName,
                            Href = AppStatic.BaseSiteUrl + linkPrefix + "/" + _secondLevCat.CategoryUrl + "/" + _secondLevCat.SecondLevCategoryUrl,
                            Title = _secondLevCat.SecondLevCategoryName,
                            SubLinks = RelatedArticleLinks(_secondLevCat, AppStatic.BaseSiteUrl + linkPrefix, 2)
                        };
                //newCat.SubLinks.AddRange(GenerateTreeViewThirdLev(_secondLevCat.SecondLevCategoryId, linkPrefix));
                linkList.Add(newCat);
                //if (String.IsNullOrEmpty(_cat.ThirdLevCategoryName))
                //{
                //    linkList.Add(new CustomHtmlLink
                //        {
                //            LinkText = _cat.SecondLevCategoryName,
                //            Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + _cat.SecondLevCategoryUrl,
                //            Title = _cat.SecondLevCategoryName,
                //            SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 2)
                //        });
                //}

                //foreach(Category __cat in thirdLevelCats){
                //    linkList.Last().SubLinks.Add(new CustomHtmlLink
                //        {
                //            LinkText = __cat.ThirdLevCategoryName,
                //            Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + __cat.SecondLevCategoryUrl + "/" + __cat.ThirdLevCategoryUrl,
                //            Title = __cat.ThirdLevCategoryName,
                //            SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 3)
                //        });
                //}
                //else
                //{
                //    linkList.Add(new CustomHtmlLink
                //    {
                //        LinkText = _cat.ThirdLevCategoryName,
                //        Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + _cat.SecondLevCategoryUrl + "/" + _cat.ThirdLevCategoryUrl,
                //        Title = _cat.ThirdLevCategoryName,
                //        SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 3)
                //    });
                //}

                //foreach (Category thirdLevCat in AppStatic.Categories
                //                                            .Where(c => c.SecondLevCategoryId == _cat.SecondLevCategoryId 
                //                                                && !String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
                //                                                .ToList())
                //{
                //    linkList.Last().SubLinks.Add(new CustomHtmlLink
                //    {
                //        LinkText = thirdLevCat.ThirdLevCategoryName,
                //        Href = "/" + linkPrefix + "/" + thirdLevCat.CategoryUrl + "/" + thirdLevCat.SecondLevCategoryUrl + "/" + thirdLevCat.ThirdLevCategoryUrl,
                //        Title = thirdLevCat.SecondLevCategoryName,
                //        SubLinks = la.RelatedArticleLinks(thirdLevCat, linkPrefix, 3)
                //    });
                //}
            }
            //if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
            //{
            //    CustomHtmlLink htmlLink = new CustomHtmlLink
            //    {
            //        LinkText = cat.ThirdLevCategoryName,
            //        Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl + "/" + cat.ThirdLevCategoryUrl,
            //        Title = cat.ThirdLevCategoryName,
            //        SubLinks = la.RelatedArticleLinks(cat, linkPrefix, 3)
            //    };
            //    //linkList[0].SubLinks=la.RelatedArticleLinks(cat, linkPrefix, 3);

            //    List<Category> secondLevelCats = AppStatic.Categories
            //                                    .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
            //                                    && c.CategoryRowId != categoryRowId).ToList();
            //    foreach (Category _cat in secondLevelCats)
            //    {
            //        //linkList.Add(new CustomHtmlLink
            //        //{
            //        //    LinkText = _cat.SecondLevCategoryName,
            //        //    Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + _cat.SecondLevCategoryUrl,
            //        //    Title = _cat.SecondLevCategoryName + "-category",
            //        //});

            //        //(!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
            //        //{

            //        //}

            //        //linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix);
            //    }
            //    return linkList;
            //}

            //if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
            //{
            //    List<Category> secondLevelCats = AppStatic.Categories
            //                                    .Where(c => c.CategoryId == cat.CategoryId).ToList();
            //    foreach (Category _cat in secondLevelCats)
            //    {
            //        linkList.Add(new CustomHtmlLink
            //        {
            //            LinkText = _cat.CategoryName,
            //            Href = "/" + linkPrefix + "/" + _cat.CategoryUrl,
            //            Title = _cat.CategoryName
            //        });
            //        if (!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
            //        {
            //            linkList.Last().SubLinks = GenerateRelatedTreeView(_cat.CategoryRowId, linkPrefix);
            //        }
            //        else
            //        {
            //            linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 3);
            //        }
            //    }
            //    return linkList;
            //}

            //ListAccess la = new ListAccess();
            //linkList = la.RelatedLinks(cat, linkPrefix);

            return linkList;
        }

        //public static List<CustomHtmlLink> GenerateTreeViewFirstLev(int categoryRowId, string linkPrefix)
        //{

        //    List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
        //    ListAccess la = new ListAccess();
        //    Category cat = AppStatic.Categories
        //                        .Where(c => c.CategoryRowId == categoryRowId)
        //                        .FirstOrDefault();

        //    if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
        //    {
        //        CustomHtmlLink htmlLink = new CustomHtmlLink
        //        {
        //            LinkText = cat.ThirdLevCategoryName,
        //            Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl + "/" + cat.ThirdLevCategoryUrl,
        //            Title = cat.ThirdLevCategoryName,
        //            SubLinks = la.RelatedArticleLinks(cat, linkPrefix, 3)
        //        };
        //        //linkList[0].SubLinks=la.RelatedArticleLinks(cat, linkPrefix, 3);

        //        List<Category> secondLevelCats = AppStatic.Categories
        //                                        .Where(c => c.SecondLevCategoryId == cat.SecondLevCategoryId
        //                                        && c.CategoryRowId != categoryRowId).ToList();
        //        foreach (Category _cat in secondLevelCats)
        //        {
        //            //linkList.Add(new CustomHtmlLink
        //            //{
        //            //    LinkText = _cat.SecondLevCategoryName,
        //            //    Href = "/" + linkPrefix + "/" + _cat.CategoryUrl + "/" + _cat.SecondLevCategoryUrl,
        //            //    Title = _cat.SecondLevCategoryName + "-category",
        //            //});

        //            //(!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
        //            //{

        //            //}

        //            //linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix);
        //        }
        //        return linkList;
        //    }

        //    if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
        //    {
        //        List<Category> secondLevelCats = AppStatic.Categories
        //                                        .Where(c => c.CategoryId == cat.CategoryId).ToList();
        //        foreach (Category _cat in secondLevelCats)
        //        {
        //            linkList.Add(new CustomHtmlLink
        //            {
        //                LinkText = _cat.CategoryName,
        //                Href = "/" + linkPrefix + "/" + _cat.CategoryUrl,
        //                Title = _cat.CategoryName
        //            });
        //            if (!String.IsNullOrEmpty(_cat.ThirdLevCategoryUrl))
        //            {
        //                linkList.Last().SubLinks = GenerateRelatedTreeView(_cat.CategoryRowId, linkPrefix);
        //            }
        //            else
        //            {
        //                linkList.Last().SubLinks = la.RelatedArticleLinks(_cat, linkPrefix, 3);
        //            }
        //        }
        //        return linkList;
        //    }

        //    //ListAccess la = new ListAccess();
        //    //linkList = la.RelatedLinks(cat, linkPrefix);

        //    return linkList;
        //}

        public static List<CustomHtmlLink> GenerateTreeViewThirdLev(Category cat, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            //ListAccess la = new ListAccess();

            List<Category> thirdLevCats = AppStatic.Categories
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
                            Href = AppStatic.BaseSiteUrl + linkPrefix + "/" + _thirdLevCat.CategoryUrl + "/" + _thirdLevCat.SecondLevCategoryUrl + "/" + _thirdLevCat.ThirdLevCategoryUrl,
                            Title = _thirdLevCat.ThirdLevCategoryName,
                            SubLinks = RelatedArticleLinks(_thirdLevCat, AppStatic.BaseSiteUrl + linkPrefix, 3)
                        };
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
                        articles = db.Articles.Where(a => a.Category.CategoryId == cat.CategoryId).ToList();
                        // whereSql = "WHERE CategoryId = " + cat.CategoryId;
                        break;
                    case 2:
                        articles = db.Articles.Where(a => a.Category.SecondLevCategoryId == cat.SecondLevCategoryId).ToList();
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

                string ahref = "<a href=\"" + AppStatic.BaseSiteUrl + articleType + cat.CategoryUrl + "/";
                //MvcHtmlString ahref = new MvcHtmlString("<a href=\"" + AppStatic.BaseSiteUrl + article.Category.CategoryUrl + "/");
                if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
                {
                    ahref += cat.SecondLevCategoryUrl + "/";
                }
                if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
                {
                    ahref += cat.ThirdLevCategoryUrl + "/";
                }
                ahref += a.URLLink + "\">" + a.Name + "</a>";

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

        //public static Category GetCategory(string catOne, string catTwo, string catThree)
        //{
        //    Category category;
        //    int categoryRowId = GetCatigoryRowId(catOne, catTwo, catThree);
        //    category = AppStatic.Categories.Where(c => c.CategoryRowId == categoryRowId).FirstOrDefault();

        //    return category;

        //    #region Old Code

        //    //if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
        //    //{
        //    //    category = AppStatic.Categories
        //    //                    .Where(c => c.CategoryUrl == catOne
        //    //                            && c.SecondLevCategoryUrl == catTwo
        //    //                            && c.ThirdLevCategoryUrl == catThree)
        //    //                    .FirstOrDefault();
        //    //}
        //    //else if (catOne.Length > 0 && catTwo.Length > 0)
        //    //{
        //    //    category = AppStatic.Categories
        //    //                    .Where(c => c.CategoryUrl == catOne
        //    //                            && c.SecondLevCategoryUrl == catTwo
        //    //                            && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
        //    //                    .FirstOrDefault();
        //    //}
        //    //else
        //    //{
        //    //    category = AppStatic.Categories
        //    //                    .Where(c => c.CategoryUrl == catOne
        //    //                            && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
        //    //                    .FirstOrDefault();
        //    //}

        //    #endregion

        //}

        //public static int GetCatigoryRowId(string url) 
        //{
        //    int categoryId = 0;
        //    if (url.StartsWith("/post") && url != "/post")
        //    {
        //        url = url.Substring(5, (url.Length - 1));
        //    }
        //    if (url.StartsWith("/"))
        //    {
        //        url = url.Substring(1, (url.Length-1));
        //    }
            
        //    string[] catigoryNames = url.Split('/');
        //    switch (catigoryNames.Count())
        //    {
        //        case 1:
        //            categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catigoryNames[0]
        //                                && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
        //                        .Select(c => c.CategoryId)
        //                        .FirstOrDefault();
        //            break;
        //        case 2:
        //            categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catigoryNames[0]
        //                                && c.SecondLevCategoryUrl == catigoryNames[1]
        //                                && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
        //                        .Select(c => c.CategoryId)
        //                        .FirstOrDefault();
        //            break;
        //        case 3:
        //            categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catigoryNames[0]
        //                                && c.SecondLevCategoryUrl == catigoryNames[1]
        //                                && c.ThirdLevCategoryUrl == catigoryNames[2])
        //                        .Select(c => c.CategoryId)
        //                        .FirstOrDefault();
        //            break;
        //        case 4:
        //            //categoryId = AppStatic.Categories
        //            //            .Where(c => c.CategoryName == catigoryNames[0] 
        //            //                && c.SecondLevCategoryName == catigoryNames[1]
        //            //                && c.ThirdLevCategoryName == catigoryNames[2]
        //            //            .Select(c => c.CategoryId)
        //            //            .FirstOrDefault();
        //            break;
        //        default:
        //            break;
        //    }

        //    return categoryId;
        //}

        //public static int GetCatigoryRowId(string catOne, string catTwo, string catThree)
        //{
        //    int categoryId = 0;

        //    if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
        //    {
        //        categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catOne
        //                                && c.SecondLevCategoryUrl == catTwo
        //                                && c.ThirdLevCategoryUrl == catThree)
        //                        .Select(c => c.CategoryRowId)
        //                        .FirstOrDefault();
        //    }
        //    else if (catOne.Length > 0 && catTwo.Length >0 )
        //    {
        //        categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catOne
        //                                && c.SecondLevCategoryUrl == catTwo
        //                                && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
        //                        .Select(c => c.CategoryRowId)
        //                        .FirstOrDefault();
        //    }
        //    else
        //    {
        //        categoryId = AppStatic.Categories
        //                        .Where(c => c.CategoryUrl == catOne
        //                                && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
        //                        .Select(c => c.CategoryRowId)
        //                        .FirstOrDefault();
        //    }


        //    return categoryId;
        //}

        public static Category GetCatigory(string catOne, string catTwo, string catThree)
        {
            Category cat = new Category();

            if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
            {
                cat = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && c.SecondLevCategoryUrl == catTwo
                                        && c.ThirdLevCategoryUrl == catThree)
                                .FirstOrDefault();
            }
            else if (catOne.Length > 0 && catTwo.Length > 0)
            {
                cat = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && c.SecondLevCategoryUrl == catTwo
                                        && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
                                .FirstOrDefault();
            }
            else
            {
                cat = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
                                .FirstOrDefault();
            }

            return cat;
        }

        public static Category GetCatigroy(int categoryRowId)
        {
            Category cat = new Category();
                cat = AppStatic.Categories
                                .Where(c => c.CategoryRowId == categoryRowId)
                                .FirstOrDefault();
                return cat;
        }

    }
}