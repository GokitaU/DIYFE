using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class ArticleComment
    {
        public int CommentId { get; set; }
        public int RepyToCommentId { get; set; }
        public int ArticleId { get; set; }
        public string PosterName { get; set; }
        public string PosterEmail { get; set; }
        public string PosterWebSite { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ArticleComment> RepyToCommentList = new List<ArticleComment>();

        public bool InsertComment(ArticleComment comment){

            string queryString = "INSERT INTO [MLB].[dbo].[ArticleComment] " +
                                  " ([ArticleId],[PosterName],[PosterEmail],[PosterWebSite],[CommentText],[RepyToCommentId]) " +
                                   " VALUES (@ArticleId,@PosterName,@PosterEmail,@PosterWebSite,@CommentText,@RepyToCommentId)";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ArticleId", comment.ArticleId);
                command.Parameters.AddWithValue("@PosterName", comment.PosterName);
                command.Parameters.AddWithValue("@PosterEmail", comment.PosterEmail);
                command.Parameters.AddWithValue("@PosterWebSite", comment.PosterWebSite);
                command.Parameters.AddWithValue("@CommentText", comment.Text);
                command.Parameters.AddWithValue("@RepyToCommentId", comment.RepyToCommentId);
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
