using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class Category
    {
        //private int _categoryRowId { get; set; }
        //private int _categoryId { get; set; }
        //private int _secondLevCategoryId { get; set; }
        //private int _thirdLevCategoryId { get; set; }

        //private string _categoryName {get;set;}
        //private string _categoryUrl { get; set; }
        //private string _secondLevCategoryName { get; set; }
        //private string _secondLevCategoryUrl { get; set; }
        //private string _thirdLevCategoryName { get; set; }
        //private string _thirdLevCategoryUrl { get; set; }

        public int CategoryRowId { get; set; }
        public int TopNavIndex { get; set; }
        public int CategoryId { get; set; }
        public int SecondLevCategoryId { get; set; }
        public int ThirdLevCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string SecondLevCategoryName { get; set; }
        public string SecondLevCategoryUrl { get; set; }
        public string ThirdLevCategoryName { get; set; }
        public string ThirdLevCategoryUrl { get; set; }

        public bool InsertCategory(Category category)
        {

            if (category.CategoryId == 0)
            {
                string maxQuery = "SELECT MAX(CategoryId) FROM Category";
                using (SqlConnection connection = new SqlConnection(Base.conn))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(maxQuery, connection);
                    //command.Parameters.AddWithValue("leagueId", leagueId);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            category.CategoryId = reader.GetInt32(0) + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        DIYError sError = new DIYError(ex);
                    }
                }
            }

            string queryString = "INSERT INTO [MLB].[dbo].[Category] " +
           "([CategoryId],[SecondLevCategoryId],[ThirdLevCategoryId],[CategoryName],[CategoryUrl],[SecondLevCategoryName],[SecondLevCategoryUrl],[ThirdLevCategoryName],[ThirdLevCategoryUrl]) " +
                "VALUES " +
           "(@CategoryId,@SecondLevCategoryId,@ThirdLevCategoryId,@CategoryName,@CategoryUrl,@SecondLevCategoryName,@SecondLevCategoryUrl,@ThirdLevCategoryName,@ThirdLevCategoryUrl)";

            if (!string.IsNullOrEmpty(category.SecondLevCategoryName) && !string.IsNullOrEmpty(category.SecondLevCategoryUrl))
            {
                if (category.SecondLevCategoryId == 0)
                {
                    string maxQuery = "SELECT MAX(SecondLevCategoryId) FROM Category";
                    using (SqlConnection connection = new SqlConnection(Base.conn))
                    {
                        // Create the Command and Parameter objects.
                        SqlCommand command = new SqlCommand(maxQuery, connection);
                        //command.Parameters.AddWithValue("leagueId", leagueId);
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                category.SecondLevCategoryId = reader.GetInt32(0) + 1;
                            }
                        }
                        catch (Exception ex)
                        {
                            DIYError sError = new DIYError(ex);
                        }
                    }
                }
            }
            else
            {
                category.SecondLevCategoryName = "";
                category.SecondLevCategoryUrl = "";
            }

            if (!string.IsNullOrEmpty(category.ThirdLevCategoryName) && !string.IsNullOrEmpty(category.ThirdLevCategoryUrl))
            {
                if (category.ThirdLevCategoryId == 0)
                {
                        string maxQuery = "SELECT MAX(ThirdLevCategoryId) FROM Category";
                        using (SqlConnection connection = new SqlConnection(Base.conn))
                        {
                            // Create the Command and Parameter objects.
                            SqlCommand command = new SqlCommand(maxQuery, connection);
                            //command.Parameters.AddWithValue("leagueId", leagueId);
                            try
                            {
                                connection.Open();
                                SqlDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    category.ThirdLevCategoryId = reader.GetInt32(0) + 1;
                                }
                            }
                            catch (Exception ex)
                            {
                                DIYError sError = new DIYError(ex);
                            }
                        }
                    }
            }
            else
            {
                category.ThirdLevCategoryName = "";
                category.ThirdLevCategoryUrl = "";
            }
            //if (string.IsNullOrEmpty(category.SecondLevCategoryUrl))
            //{
            //    category.SecondLevCategoryUrl = "";
            //}

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                command.Parameters.AddWithValue("@SecondLevCategoryId", category.SecondLevCategoryId);
                command.Parameters.AddWithValue("@ThirdLevCategoryId", category.ThirdLevCategoryId);
                command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                command.Parameters.AddWithValue("@CategoryUrl", category.CategoryUrl);
                command.Parameters.AddWithValue("@SecondLevCategoryName", category.SecondLevCategoryName);
                command.Parameters.AddWithValue("@SecondLevCategoryUrl", category.SecondLevCategoryUrl);
                command.Parameters.AddWithValue("@ThirdLevCategoryName", category.ThirdLevCategoryName);
                command.Parameters.AddWithValue("@ThirdLevCategoryUrl", category.ThirdLevCategoryUrl);
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
