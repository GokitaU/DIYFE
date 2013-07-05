using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIYFELib
{
    public static class Tracking
    {
        //public int TrackingId;
        //public string Session, IP, URL;
        //public DateTime CreatedDate;

        //public Tracking(string session, string ip, string url){
        //    this.Session = session;
        //    this.IP = ip;
        //    this.URL = url;
        //}

        //public Tracking(string session, string ip, string url, bool insert)
        //{
        //    this.Session = session;
        //    this.IP = ip;
        //    this.URL = url;
        //    if (insert)
        //    {
        //        this.InsertTracking();
        //    }
        //}

        public static void InsertTracking(string session, string ip, string url)
        {
            string queryCheckGame = "INSERT INTO [dbo].[Tracking] " +
                                           "([Session] ,[IP] ,[URL]) " +
                                     "VALUES (@Session ,@IP ,@URL)";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                SqlCommand command = new SqlCommand(queryCheckGame, connection);
                command.Parameters.AddWithValue("@Session", session);
                command.Parameters.AddWithValue("@IP", ip);
                command.Parameters.AddWithValue("@URL", url);
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
        }
        
        //TODO: THIS NEEDS TO BE UPDATED WHEN sp IS CALLED TO PULL ARTICLE
        //THIS ENITY FRAME WORK STUFF IS SLOW AND LAME, NEEDS BE REMOVE ONCE SITE IS RUNNING
        //DON'T FORGET TO REMOVE ALL INSTANCES CALLING
        public static void InsertArticleViewRequest(int articleId)
        {
            string queryCheckGame = "UPDATE [dbo].[Article] SET [ViewRequests] = [ViewRequests]+1 WHERE ArticleId = @ArticleId";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                SqlCommand command = new SqlCommand(queryCheckGame, connection);
                command.Parameters.AddWithValue("@ArticleId", articleId);
                
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
        }
    }
}
