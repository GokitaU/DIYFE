using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.UI.HtmlControls;
using System.Web.Mvc;

namespace DIYFELib
{
    public class ListAccess
    {
        public List<Category> AllCategory()
        {
            List<Category> categoryList = new List<Category>();
            string queryString = "SELECT * FROM Category";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("leagueId", leagueId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.CategoryRowId = reader.GetInt32Safe(0);
                        category.TopNavIndex = reader.GetInt32Safe(1);
                        category.CategoryId = reader.GetInt32Safe(2);
                        category.SecondLevCategoryId = reader.GetInt32Safe(3);
                        category.ThirdLevCategoryId = reader.GetInt32Safe(4);
                        category.CategoryName = reader.GetStringSafe(5);
                        category.CategoryUrl = reader.GetStringSafe(6);
                        category.SecondLevCategoryName = reader.GetStringSafe(7);
                        category.SecondLevCategoryUrl = reader.GetStringSafe(8);
                        category.ThirdLevCategoryName = reader.GetStringSafe(9);
                        category.ThirdLevCategoryUrl = reader.GetStringSafe(10);

                        categoryList.Add(category);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }
            return categoryList;
        }

        public List<CustomHtmlLink> MostViewed(int categoryId, int returnAmount)
        {
            List<CustomHtmlLink> viewList = new List<CustomHtmlLink>();

            string queryString = "MostViewed";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@returnAmount", returnAmount);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CustomHtmlLink link = new CustomHtmlLink();
                        link.Href = "/" + reader.GetStringSafe(0);
                        if (!reader.IsDBNull(1)) {
                            link.Href += reader.GetStringSafe(1);
                        }
                        if (!reader.IsDBNull(2)) {
                            link.Href += reader.GetStringSafe(2);
                        }
                        link.LinkText = reader.GetStringSafe(3);
                        link.Title = reader.GetStringSafe(4);
                        viewList.Add(link);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }

            return viewList;
        }

        public ArticleList PostList(int categoryRowId, int currentPage, int numberPerPage, int categoryLev)
        {
            ArticleList al = new ArticleList();
            al.CurrentPage = currentPage;

            //dataList.ListItems = new List<DataListItem>();
            //dataList.ListItems.Add(new DataListItem { });
            //dataList.ListItems.Add(new DataListItem { });
            //dataList.ListItems.Add(new DataListItem { });
            //dataList.ListItems.Add(new DataListItem { });
            //dataList.ListItems.Add(new DataListItem { });

            al.TotalItems = 25;

            return al;
        }

        public List<CustomHtmlLink> RelatedArticleLinks(Category cat, string linkPrefix, int categoryLevel)
        {
            List<CustomHtmlLink> linkList = new List<CustomHtmlLink>();

            //List<Article> articles = new List<Article>();
            string queryString = "SELECT [Name],[Title],[URLLink] FROM [MLB].[dbo].[Article]";
            string whereSql = "";
            switch (categoryLevel)
            {
                case 1:
                    whereSql = "WHERE CategoryId = " + cat.CategoryId;
                    break;
                case 2:
                    whereSql = "WHERE SecondLevCategoryId = " + cat.SecondLevCategoryId + " AND CategoryId= " + cat.CategoryId + " AND ThirdLevCategoryId = 0";
                    break;
                case 3:
                    whereSql = "WHERE ThirdLevCategoryId = " + cat.ThirdLevCategoryId;
                    break;
                case 4:
                    whereSql = "WHERE CategoryId = " + cat.CategoryId;
                    break;
                default:
                    break;
            }
            queryString += whereSql;

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("leagueId", leagueId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CustomHtmlLink link = new CustomHtmlLink
                        {
                            LinkText = reader.GetString(0),
                            Title = reader.GetString(1),
                            Href = linkPrefix  + reader.GetString(2)
                        };

                        linkList.Add(link);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }

            return linkList;


            //if (!String.IsNullOrEmpty(cat.ThirdLevCategoryUrl))
            //{
            //    CustomHtmlLink link = new CustomHtmlLink();
            //    link.LinkText = cat.ThirdLevCategoryName;
            //    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl + "/" + cat.ThirdLevCategoryUrl;
            //    link.Title = cat.ThirdLevCategoryName;
            //    linkList.Add(link);

            //    foreach (Article art in CategoryArticles(cat.ThirdLevCategoryId, 3))
            //    {

            //    }
            //    //List<Article> articles = 
                

            //    return linkList;
            //}

            //if (!String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
            //{
            //    //List<Article> articles = 
            //    CustomHtmlLink link = new CustomHtmlLink();
            //    link.LinkText = cat.SecondLevCategoryName;
            //    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl;
            //    link.Title = cat.SecondLevCategoryName;
            //    linkList.Add(link);

            //    return linkList;
            //}

            //if (!String.IsNullOrEmpty(cat.CategoryUrl))
            //{
            //    //List<Article> articles = 
            //    CustomHtmlLink link = new CustomHtmlLink();
            //    link.LinkText = cat.CategoryName;
            //    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl;
            //    link.Title = cat.CategoryName;
            //    linkList.Add(link);

            //    return linkList;
            //}

                //if (!String.IsNullOrEmpty(cat.CategoryUrl))
                //{
                //    CustomHtmlLink link = new CustomHtmlLink();
                //    link.LinkText = cat.CategoryName;
                //    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl;
                //    link.Title = cat.CategoryName;
                //    linkList.Add(link);
                //}
                //if (String.IsNullOrEmpty(cat.SecondLevCategoryUrl))
                //{
                //    CustomHtmlLink link = new CustomHtmlLink();
                //    link.LinkText = cat.SecondLevCategoryName;
                //    link.Href = "/" + linkPrefix + "/" + cat.CategoryUrl + "/" + cat.SecondLevCategoryUrl;
                //    link.Title = cat.SecondLevCategoryName;
                //    linkList.Add(link);
                //}
                
      

            //return linkList;
        }

        public List<Article> CategoryArticles(int categoryId, int categoryLevel)
        {
            List<Article> articles = new List<Article>();
            string queryString = "SELECT * FROM [Article]";
            string whereSql = "";
            switch (categoryLevel)
            {
                case 1:
                    whereSql = "WHERE CategoryId = " + categoryId;
                    break;
                case 2:
                    whereSql = "WHERE SecondLevCategoryId = " + categoryId;
                    break;
                case 3:
                    whereSql = "WHERE ThirdLevCategoryId = " + categoryId;
                    break;
                case 4:
                    whereSql = "WHERE CategoryId = " + categoryId;
                    break;
                default:
                    break;
            }
            queryString += whereSql;

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("leagueId", leagueId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Article article = new Article();

                        articles.Add(article);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }

            return articles;
        }

        public List<ArticleComment> ArticleComments(int articleId)
        {
            List<ArticleComment> comments = new List<ArticleComment>();

            string queryString = "SELECT [CommentId],[ArticleId],[CommentText],[CreatedDate],[RepyToCommentId] FROM [MLB].[dbo].[ArticleComment] WHERE ArticleId = " + articleId  + " ORDER BY CreatedDate DESC";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("leagueId", leagueId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comments.Add(new ArticleComment
                        {
                            CommentId = reader.GetInt32(0),
                            ArticleId = reader.GetInt32(1),
                            Text = reader.GetStringSafe(2),
                            CreatedDate = reader.GetDateTime(3),
                            RepyToCommentId = reader.GetInt32Safe(4)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }

            return comments;
        }

        //public List<Article> MostViewed(string PostType, int returnAmount)
        //{
        //    List<Article> viewList = new List<Article>();

        //    string queryString = "MostViewed";

        //    using (SqlConnection connection = new SqlConnection(Base.conn))
        //    {
        //        // Create the Command and Parameter objects.
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        //command.Parameters.AddWithValue("leagueId", leagueId);
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                Article article = new Article();
                        
        //                viewList.Add(article);
        //            }
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            DIYError sError = new DIYError(ex);
        //        }
        //    }

        //    return viewList;
        //}
    }
}
