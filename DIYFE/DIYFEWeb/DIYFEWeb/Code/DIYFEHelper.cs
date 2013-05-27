using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFEWeb.Models;
using DIYFELib;

namespace DIYFEWeb.Code
{
    public static class DIYFEHelper
    {
        //public static List<CustomHtmlLink> GenerateCrumbLinks(string Url)
        //{
        //    List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            
        //    return linkList;
        //}

        public static List<CustomHtmlLink> GenerateCrumbLinks(int categoryRowId, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            Category cat = AppStatic.Categories
                                .Where(c => c.CategoryRowId == categoryRowId)
                                .FirstOrDefault();
            if (cat != null)
            {
                if (!String.IsNullOrEmpty(cat.CategoryUrl))
                {
                    CustomHtmlLink link = new CustomHtmlLink();
                    link.LinkText = cat.CategoryName;
                    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl;
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

        public static List<CustomHtmlLink> GenerateRelatedTreeView(int categoryRowId, string linkPrefix)
        {
            
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            ListAccess la = new ListAccess();
            Category cat = AppStatic.Categories
                                .Where(c => c.CategoryRowId == categoryRowId)
                                .FirstOrDefault();
            linkList.Add(new CustomHtmlLink
               {
                    LinkText = cat.CategoryName,
                    Href = AppStatic.BaseSiteUrl + linkPrefix + "/" + cat.CategoryUrl,
                    Title = cat.CategoryName,
                    SubLinks = GenerateTreeViewSecondLev(categoryRowId, linkPrefix)//la.RelatedArticleLinks(cat, linkPrefix, 3)
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

        public static List<CustomHtmlLink> GenerateTreeViewSecondLev(int firstLevCategoryId, string linkPrefix)
        {

            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            ListAccess la = new ListAccess();
            //Category cat = AppStatic.Categories
            //                    .Where(c => c.CategoryRowId == categoryRowId)
            //                    .FirstOrDefault();

            List<Category> secondLevCats = AppStatic.Categories
                                                .Where(c => c.CategoryId == firstLevCategoryId
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
                            SubLinks = la.RelatedArticleLinks(_secondLevCat, AppStatic.BaseSiteUrl + linkPrefix, 2)
                        };
                newCat.SubLinks.AddRange(GenerateTreeViewThirdLev(_secondLevCat.SecondLevCategoryId, linkPrefix));
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

        public static List<CustomHtmlLink> GenerateTreeViewThirdLev(int secondLevCatId, string linkPrefix)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();
            ListAccess la = new ListAccess();

            List<Category> thirdLevCats = AppStatic.Categories
                                                .Where(c => c.SecondLevCategoryId == secondLevCatId
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
                            SubLinks = la.RelatedArticleLinks(_thirdLevCat, AppStatic.BaseSiteUrl + linkPrefix, 3)
                        };
                linkList.Add(newCat);
                //newCat.SubLinks.AddRange(GenerateTreeViewThirdLev(_secondLevCat.SecondLevCategoryId, linkPrefix));
            }

            return linkList;
        }

        public static Category GetCategory(string catOne, string catTwo, string catThree)
        {
            Category category;
            int categoryRowId = GetCatigoryRowId(catOne, catTwo, catThree);
            category = AppStatic.Categories.Where(c => c.CategoryRowId == categoryRowId).FirstOrDefault();

            return category;

            #region Old Code

            //if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
            //{
            //    category = AppStatic.Categories
            //                    .Where(c => c.CategoryUrl == catOne
            //                            && c.SecondLevCategoryUrl == catTwo
            //                            && c.ThirdLevCategoryUrl == catThree)
            //                    .FirstOrDefault();
            //}
            //else if (catOne.Length > 0 && catTwo.Length > 0)
            //{
            //    category = AppStatic.Categories
            //                    .Where(c => c.CategoryUrl == catOne
            //                            && c.SecondLevCategoryUrl == catTwo
            //                            && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
            //                    .FirstOrDefault();
            //}
            //else
            //{
            //    category = AppStatic.Categories
            //                    .Where(c => c.CategoryUrl == catOne
            //                            && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
            //                    .FirstOrDefault();
            //}

            #endregion

        }

        public static int GetCatigoryRowId(string url) 
        {
            int categoryId = 0;
            if (url.StartsWith("/post") && url != "/post")
            {
                url = url.Substring(5, (url.Length - 1));
            }
            if (url.StartsWith("/"))
            {
                url = url.Substring(1, (url.Length-1));
            }
            
            string[] catigoryNames = url.Split('/');
            switch (catigoryNames.Count())
            {
                case 1:
                    categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catigoryNames[0]
                                        && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
                                .Select(c => c.CategoryId)
                                .FirstOrDefault();
                    break;
                case 2:
                    categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catigoryNames[0]
                                        && c.SecondLevCategoryUrl == catigoryNames[1]
                                        && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
                                .Select(c => c.CategoryId)
                                .FirstOrDefault();
                    break;
                case 3:
                    categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catigoryNames[0]
                                        && c.SecondLevCategoryUrl == catigoryNames[1]
                                        && c.ThirdLevCategoryUrl == catigoryNames[2])
                                .Select(c => c.CategoryId)
                                .FirstOrDefault();
                    break;
                case 4:
                    //categoryId = AppStatic.Categories
                    //            .Where(c => c.CategoryName == catigoryNames[0] 
                    //                && c.SecondLevCategoryName == catigoryNames[1]
                    //                && c.ThirdLevCategoryName == catigoryNames[2]
                    //            .Select(c => c.CategoryId)
                    //            .FirstOrDefault();
                    break;
                default:
                    break;
            }

            return categoryId;
        }

        public static int GetCatigoryRowId(string catOne, string catTwo, string catThree)
        {
            int categoryId = 0;

            if (catOne.Length > 0 && catTwo.Length > 0 && catThree.Length > 0)
            {
                categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && c.SecondLevCategoryUrl == catTwo
                                        && c.ThirdLevCategoryUrl == catThree)
                                .Select(c => c.CategoryRowId)
                                .FirstOrDefault();
            }
            else if (catOne.Length > 0 && catTwo.Length >0 )
            {
                categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && c.SecondLevCategoryUrl == catTwo
                                        && String.IsNullOrEmpty(c.ThirdLevCategoryUrl))
                                .Select(c => c.CategoryRowId)
                                .FirstOrDefault();
            }
            else
            {
                categoryId = AppStatic.Categories
                                .Where(c => c.CategoryUrl == catOne
                                        && String.IsNullOrEmpty(c.SecondLevCategoryUrl))
                                .Select(c => c.CategoryRowId)
                                .FirstOrDefault();
            }


            return categoryId;
        }
    }
}