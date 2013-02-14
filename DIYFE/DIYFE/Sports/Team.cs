using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public class Team
    {
       
        public int TeamId { get; set; }

        public int LeagueId { get; set; }
        public string League { get; set; }
        
        public int ConferenceId { get; set; }
        public string Conference { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string ScrapId { get; set; }
        public string ScrapUrl { get; set; }
        public string Scrap2Url { get; set; }

        public Team() { }

        public Team(int teamId)
        {
            string queryString = "SELECT * FROM [dbo].[Teams] WHERE TeamId = @teamId";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("teamId", teamId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        this.TeamId = reader.GetInt32(0);
                        this.LeagueId = reader.GetInt32(1);
                        this.League = reader.GetString(2);
                        this.ConferenceId = reader.GetInt32(3);
                        this.Conference = reader.GetString(4);
                        this.Name = reader.GetString(5);
                        this.City = reader.GetString(6);
                        this.ScrapId = reader.GetString(7);
                        this.ScrapUrl = reader.GetString(8);
                        this.Scrap2Url = reader.GetString(9);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex, teamId);
                }
            }
        }

        public bool InsertTeam()
        {
            string queryString = "INSERT INTO [dbo].[Team] "
                                       + "([LeagueId]"
                                       + ",[ConferenceId]"
                                       + ",[Name]"
                                       + ",[City]"
                                       + ",[ScrapId]"
                                       + ",[ScrapUrl]"
                                       + ",[Scrap2Url])"
                                 + " VALUES "
                                       + "(@LeagueId"
                                       + "(@ConferenceId"
                                       + ",@City"
                                       + ",@ScrapId"
                                       + ",@ScrapUrl"
                                       + ",@Scrap2Url)";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@LeagueId", this.LeagueId);
                command.Parameters.AddWithValue("@ConferenceId", this.ConferenceId);
                command.Parameters.AddWithValue("@Name", this.Name);
                command.Parameters.AddWithValue("@City", this.City);
                command.Parameters.AddWithValue("@ScrapId", this.ScrapId);
                command.Parameters.AddWithValue("@ScrapUrl", this.ScrapUrl);
                command.Parameters.AddWithValue("@Scrap2Url", this.Scrap2Url);

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

        public bool UpdateTeam()
        {
            if (this.TeamId != 0)
            {
                string queryString = "UPDATE [dbo].[Team] "
                                        + " SET [Name] = @Name"
                                        + ",[City] = @City"
                                          + ",[ScrapId] = @ScrapId"
                                          + ",[ScrapUrl] = @ScrapUrl"
                                          + ",[Scrap2Url] = @Scrap2Url"
                                     + " WHERE TeamId = @TeamId";

                using (SqlConnection connection = new SqlConnection(Base.conn))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@TeamId", this.TeamId);
                    command.Parameters.AddWithValue("@Name", this.Name);
                    command.Parameters.AddWithValue("@City", this.City);
                    command.Parameters.AddWithValue("@ScrapId", this.ScrapId);
                    command.Parameters.AddWithValue("@ScrapUrl", this.ScrapUrl);
                    command.Parameters.AddWithValue("@Scrap2Url", this.Scrap2Url);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        SportsError sError = new SportsError(ex, this.TeamId);
                    }
                }
            }
            return true;
        }

    }
}
