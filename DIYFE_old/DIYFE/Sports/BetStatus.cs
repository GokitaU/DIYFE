using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public class BetStatus
    {
        public int BetStatusId { get; set; }
        public int BetId { get; set; }
        public int BetStatusTypeId { get; set; }

        public bool InsertBetStatus()
        {
            string query = "INSERT INTO [dbo].[BetStatus] " +
                                   "([BetId] " +
                                   ",[BetStatusTypeId]) " +
                              " VALUES " + 
                                   "(@BetId " +
                                   ",@BetStatusTypeId)";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BetId", this.BetId);
                command.Parameters.AddWithValue("@BetStatusTypeId", this.BetStatusTypeId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        SportsError sError = new SportsError(ex);
                    }

            }

            return true;
        }

    }
}
