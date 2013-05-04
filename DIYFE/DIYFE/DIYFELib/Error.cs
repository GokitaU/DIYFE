using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class DIYError
    {
        public DIYError(Exception err)
        {
            InsertError(err, 0);
        }
        public DIYError(Exception err, int refId)
        {
            InsertError(err, refId);
        }


        public void InsertError(Exception err)
        {
            InsertError(err, 0);
        }

        public void InsertError(Exception err, int refId)
        {
            string conn = "Data Source=diyfe.org;Initial Catalog=MLB;Persist Security Info=True;User ID=jbt411;Password=ZigZag15";
            string queryString = "INSERT INTO [dbo].[Error] ([RefId],[ErrorMethod],[ErrorText],[ErrorDate]) VALUES (@RefId,@ErrorMethod,@ErrorText,@ErrorDate)";

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(conn))
            {
                // Create the Command and Parameter objects.
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@RefId", refId);
                command.Parameters.AddWithValue("@ErrorMethod", err.Source);
                command.Parameters.AddWithValue("@ErrorText", err.Message + " -- " + err.InnerException);
                command.Parameters.AddWithValue("@ErrorDate", DateTime.Now);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                { }
            }
        }
    }
}
