using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class Article : Category
    {
        public int ArticleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public ArticleType Articletype { get; set; }
        //public Category ArticleCategory { get; set; }
        //public int CategoryId { get; set; }
        //public int SecondLevCategoryId { get; set; }
        //public int ThirdLevCategoryId { get; set; }
        public int ViewRequests { get; set; }

        public string URLLink {get;set;}
        public string NameId  {get;set;}

        //SEO DATA
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }

        //MAIN CONTENT
        public string MainContent { get; set; }
        public string SideContent { get; set; }
        public string ListItemContent { get; set; }
        public string Name { get; set; }

        public void LoadArticle(string articleName, int articleId)
        {
            string queryString = "sp_LoadArticle";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (!String.IsNullOrEmpty(articleName))
                {
                    command.Parameters.Add(new SqlParameter("@articleName", articleName));
                }
                if (articleId > 0)
                {
                    command.Parameters.Add(new SqlParameter("@articleId", articleName));
                }

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    LoadArticle(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }
        }

        //public void LoadArticle(int articleId)
        //{
        //    string queryString = "SELECT [ArticleId],[CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name] " +
        //                                ",[Title],[Description],[Author],[Keywords],[MainContent],[SideContent] " +
        //                                ",[ListItemContent],[ViewRequests],[URLLink] " +
        //                        "FROM [MLB].[dbo].[Article] " + 
        //                        "WHERE ArticleId = " + articleId;

        //    using (SqlConnection connection = new SqlConnection(Base.conn))
        //    {
        //        // Create the Command and Parameter objects.
        //        SqlCommand command = new SqlCommand(queryString, connection);

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            LoadArticle(reader);
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            DIYError sError = new DIYError(ex);
        //        }
        //    }

        //}

        //public void LoadArticle(int categoryId, int categoryLevel)
        //{
        //    string queryString = "SELECT [ArticleId],[CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name] " +
        //                                ",[Title],[Description],[Author],[Keywords],[MainContent],[SideContent] " +
        //                                ",[ListItemContent],[ViewRequests],[URLLink] " +
        //                        "FROM [MLB].[dbo].[Article] ";

        //    switch (categoryLevel)
        //    {
        //        case 1:
        //            queryString += "WHERE CategoryId = " + categoryId;
        //            break;
        //        case 2:
        //            queryString += "WHERE SecondLevCategoryId = " + categoryId;
        //            break;
        //        case 3:
        //            queryString += "WHERE ThirdCategoryId = " + categoryId;
        //            break;
        //    }

        //    using (SqlConnection connection = new SqlConnection(Base.conn))
        //    {
        //        // Create the Command and Parameter objects.
        //        SqlCommand command = new SqlCommand(queryString, connection);

        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            LoadArticle(reader);
        //            reader.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            DIYError sError = new DIYError(ex);
        //        }
        //    }
        //}

        private void LoadArticle(SqlDataReader reader){
          
            while (reader.Read())
            {
                this.ArticleId = reader.GetInt32(0);
                
                this.Name = reader.GetString(1);
                this.Title = reader.GetStringSafe(2);
                this.Description = reader.GetStringSafe(3);
                this.Author = reader.GetStringSafe(4);
                this.Keywords = reader.GetStringSafe(5);
                this.MainContent = reader.GetStringSafe(6);
                this.ViewRequests = reader.GetInt32(7);
                this.URLLink = reader.GetString(8);
                this.NameId = reader.GetString(9);
                this.CategoryRowId = reader.GetInt32(10);
                this.CategoryId = reader.GetInt32Safe(11);
                this.SecondLevCategoryId = reader.GetInt32Safe(12);
                this.ThirdLevCategoryId = reader.GetInt32Safe(13);
                this.CategoryName = reader.GetString(14);
                this.CategoryUrl = reader.GetString(15);
                this.SecondLevCategoryName = reader.GetStringSafe(16);
                this.SecondLevCategoryUrl = reader.GetStringSafe(17);
                this.ThirdLevCategoryName = reader.GetStringSafe(18);
                this.ThirdLevCategoryUrl = reader.GetStringSafe(19);
                this.CreatedDate = reader.GetDateTime(20);
            }
        }

        public bool InsertArticle(Article article)
        {

            string queryString = "INSERT INTO [MLB].[dbo].[Article] " +
                                 "([CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name],[Title],[Description],[Author],[Keywords],[MainContent],[SideContent],[ListItemContent],[ViewRequests],[URLLink],[NameId]) " +
                                    "VALUES " +
                                 "(@CategoryId,@SecondLevCategoryId,@ThirdLevCategoryId,@Name,@Title,@Description,@Author,@Keywords,@MainContent,@SideContent,@ListItemContent,@ViewRequests,@URLLink,@NameId)";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CategoryId", article.CategoryId);
                command.Parameters.AddWithValue("@SecondLevCategoryId", article.SecondLevCategoryId);
                command.Parameters.AddWithValue("@ThirdLevCategoryId", article.ThirdLevCategoryId);
                command.Parameters.AddWithValue("@Name", article.Name);
                command.Parameters.AddWithValue("@Title", article.Title);
                command.Parameters.AddWithValue("@Description", article.Description);
                command.Parameters.AddWithValue("@Author", article.Author);
                command.Parameters.AddWithValue("@Keywords", article.Keywords);
                command.Parameters.AddWithValue("@MainContent", article.MainContent);
                command.Parameters.AddWithValue("@SideContent", article.SideContent);
                command.Parameters.AddWithValue("@ListItemContent", article.ListItemContent);
                command.Parameters.AddWithValue("@ViewRequests", article.ViewRequests);
                command.Parameters.AddWithValue("@URLLink", article.URLLink);
                command.Parameters.AddWithValue("@NameId", article.NameId);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    DIYError sError = new DIYError(ex);
                }
            }

            return true;
        }

    }
}
