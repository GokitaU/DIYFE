using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4Template.Models
{
    public class ErrorModel
    {
        public void InsertError(Exception err)
        {
            InsertError(err, 0, true);
        }

        public void InsertErrorNoMail(Exception err)
        {
            InsertError(err, 0, false);
        }

        public void InsertError(Exception err, int refId, bool sendMail)
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;
            string queryString = "INSERT INTO [MLB].[dbo].[Error] ([RefId],[ErrorMethod],[ErrorText],[ErrorDate]) VALUES (@RefId,@ErrorMethod,@ErrorText,@ErrorDate)";

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(conn))
            {
                // Create the Command and Parameter objects.
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@RefId", refId);
                command.Parameters.AddWithValue("@ErrorMethod", err.Source);
                command.Parameters.AddWithValue("@ErrorText", err.InnerException);
                command.Parameters.AddWithValue("@ErrorDate", DateTime.Now);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                { }
            }

            if (sendMail)
            {
                var email = EmailMessageFactory.GetErrorEmail(err);
                var result = EmailClient.SendEmail(email);
            }

        }
    }
}