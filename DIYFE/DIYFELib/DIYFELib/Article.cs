using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class Article
    {
        public int ArticleId { get; set; }
        public int CategoryId { get; set; }
        public int SecondLevCategoryId { get; set; }
        public int ThirdLevCategoryId { get; set; }
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



        public void LoadArticle(string articleName)
        {
            string queryString = "SELECT [ArticleId],[CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name] " +
                                        ",[Title],[Description],[Author],[Keywords],[MainContent],[SideContent] " +
                                        ",[ListItemContent],[ViewRequests],[URLLink] " +
                                "FROM [MLB].[dbo].[Article] " +
                                "WHERE NameId = '" + articleName + "'";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

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

        public void LoadArticle(int articleId)
        {
            string queryString = "SELECT [ArticleId],[CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name] " +
                                        ",[Title],[Description],[Author],[Keywords],[MainContent],[SideContent] " +
                                        ",[ListItemContent],[ViewRequests],[URLLink] " +
                                "FROM [MLB].[dbo].[Article] " + 
                                "WHERE ArticleId = " + articleId;

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

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

        public void LoadArticle(int categoryId, int categoryLevel)
        {
            string queryString = "SELECT [ArticleId],[CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[Name] " +
                                        ",[Title],[Description],[Author],[Keywords],[MainContent],[SideContent] " +
                                        ",[ListItemContent],[ViewRequests],[URLLink] " +
                                "FROM [MLB].[dbo].[Article] ";

            switch (categoryLevel)
            {
                case 1:
                    queryString += "WHERE CategoryId = " + categoryId;
                    break;
                case 2:
                    queryString += "WHERE SecondLevCategoryId = " + categoryId;
                    break;
                case 3:
                    queryString += "WHERE ThirdCategoryId = " + categoryId;
                    break;
            }

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

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

        private void LoadArticle(SqlDataReader reader){
          
            while (reader.Read())
            {
                this.ArticleId = reader.GetInt32(0);
                this.CategoryId = reader.GetInt32Safe(1);
                this.SecondLevCategoryId = reader.GetInt32Safe(2);
                this.ThirdLevCategoryId = reader.GetInt32Safe(3);
                this.Name = reader.GetString(4);
                this.Title = reader.GetStringSafe(5);
                this.Description = reader.GetStringSafe(6);
                this.Author = reader.GetStringSafe(7);
                this.Keywords = reader.GetStringSafe(8);
                this.MainContent = reader.GetStringSafe(9);
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
