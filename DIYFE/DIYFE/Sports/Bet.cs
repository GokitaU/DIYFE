using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace Sports
{
    public class Bet
    {
        public int BetId { get; set; }
        
        public DateTime BetADate { get; set; }
        public DateTime BetBDate { get; set; }
        public DateTime BetCDate { get; set; }
        public DateTime BetDDate { get; set; }
        public DateTime LossDate { get; set; }

        public int BetAGameId { get; set; }
        public int BetBGameId { get; set; }
        public int BetCGameId { get; set; }
        public int BetDGameId { get; set; }

        public List<BetStatus> BetStatus { get; set; }

        public string BetStatusText { get; set; }
        public int StatusId { get; set; }
        public string WinningBet { get; set; }
        public int WinningBetTeamId { get; set; }
        public decimal WinningBetTeamRPI { get; set; }
        public int LosingBetTeamId { get; set; }
        public decimal LosingBetTeamRPI { get; set; }
        public int HomeTeamId { get; set; }
        public string BetNote { get; set; }
        public string WinningTeamName { get; set; }
        public string LossingTeamName { get; set; }
        public int SeasonId { get; set; }

        public bool WinA { get; set; }
        public bool WinB { get; set; }
        public bool WinC { get; set; }
        public bool WinD { get; set; }
        public bool Loss { get; set; }

        public List<Game> BetGames { get; set; }

        public Bet() { }

        public Bet(int betId)
        {

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand("sp_BetDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@betId", betId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        this.BetId = betId; //(0)
                        this.BetADate = reader.GetDateSafe(1);
                        this.BetBDate = reader.GetDateSafe(2);
                        this.BetCDate = reader.GetDateSafe(3);
                        this.BetDDate = reader.GetDateSafe(4);
                        this.BetStatusText = reader.GetString(5);
                        //this.StatusId = reader.GetInt32(5);
                        //this.StatusId = 0;
                        this.WinningBet = reader.GetStringSafe(6);
                        this.WinningBetTeamId = reader.GetInt32(7);
                        this.LosingBetTeamId = reader.GetInt32(8);
                        this.BetNote = reader.GetStringSafe(9);
                        this.WinningBetTeamRPI = reader.GetDecimalSafe(12);
                        this.LosingBetTeamRPI = reader.GetDecimalSafe(13);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }

            }
        }

        public bool InsertBet()
        {

            //Bet betCheck = new Bet();
            //string queryCheckGame = "SELECT [BetId]"
            //                        + " FROM [dbo].[Bet]"
            //                        + " WHERE WinningBetTeamId = @WinTeamId AND LosingBetTeamId = @LoseTeamId AND BetADate = @BetADate";

            //using (SqlConnection connectionCheck = new SqlConnection(Base.conn))
            //{
            //    // Create the Command and Parameter objects.
            //    SqlCommand commandCheck = new SqlCommand(queryCheckGame, connectionCheck);
            //    commandCheck.Parameters.AddWithValue("@LoseTeamId", this.LosingBetTeamId);
            //    commandCheck.Parameters.AddWithValue("@WinTeamId", this.WinningBetTeamId);
            //    commandCheck.Parameters.AddWithValue("@BetADate", this.BetADate);
            //    try
            //    {
            //        connectionCheck.Open();
            //        SqlDataReader reader = commandCheck.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            betCheck.BetId = reader.GetInt32(0);
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw (ex);
            //    }
            //}

            //if (betCheck.BetId != 0)
            //{
            //    this.BetId = betCheck.BetId;
            //}
            string queryString = "";
            if (this.BetId == 0)
            {
                queryString = "INSERT INTO [dbo].[Bet]"
                                          + "([BetADate]"
                                          + ",[BetBDate]"
                                          + ",[BetCDate]"
                                          + ",[BetDDate]"
                                          + ",[BetAGameId]"
                                          + ",[BetBGameId]"
                                          + ",[BetCGameId]"
                                          + ",[BetDGameId]"
                                          + ",[BetNote]"
                                          + ",[SeasonId])"
                                    + " VALUES "
                                          + "(@BetADate"
                                          + ",@BetBDate"
                                          + ",@BetCDate"
                                          + ",@BetDDate"
                                          + ",@BetAGameId"
                                          + ",@BetBGameId"
                                          + ",@BetCGameId"
                                          + ",@BetDGameId"
                                          + ",@BetNote"
                                          + ",@SeasonId)"
                                    + " SET @betId = @@Identity";
            }
            else
            {
                //queryString = "UPDATE [dbo].[Bet]"
                //                     + "  SET [BetADate] = @BetADate"
                //                         + " ,[BetBDate] = @BetBDate"
                //                         + " ,[BetCDate] = @BetCDate"
                //                         + " ,[BetDDate] = @BetDDate"
                //                         + " ,[WinningBet] = @WinningBet"
                //                         + " ,[WinningBetTeamId] = @WinningBetTeamId"
                //                         + " ,[LosingBetTeamId] = @LosingBetTeamId"
                //                         + " ,[BetNote] = @BetNote"
                //                    + " WHERE BetId = " + this.BetId.ToString()
                //                    + " SET @betId = " + this.BetId.ToString();
                // if (this.WinningBet != "Scheduled")
                // {
                queryString = "UPDATE [dbo].[Bet]"
                                    + "  SET [WinningBet] = @WinningBet"
                                        + " ,[WinningBetTeamId] = @WinningBetTeamId"
                                        + " ,[LosingBetTeamId] = @LosingBetTeamId"

                                   + " WHERE BetId = " + this.BetId.ToString()
                                   + " SET @betId = " + this.BetId.ToString();
                // }
                //else
                //{
                //    queryString = "UPDATE [dbo].[Bet]"
                //                        + "  SET [WinningBet] = @WinningBet"
                //                            + " ,[WinningBetTeamId] = @WinningBetTeamId"
                //                            + " ,[LosingBetTeamId] = @LosingBetTeamId"

                //                       + " WHERE BetId = " + this.BetId.ToString()
                //                       + " SET @betId = " + this.BetId.ToString();
                //}
            }
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                if (String.IsNullOrEmpty(this.BetNote)) { this.BetNote = ""; }
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@BetADate", this.BetADate);
                command.Parameters.AddWithValue("@BetBDate", this.BetBDate);
                command.Parameters.AddWithValue("@BetCDate", this.BetCDate);
                command.Parameters.AddWithValue("@BetDDate", this.BetDDate);
                command.Parameters.AddWithValue("@BetAGameId", this.BetAGameId);
                command.Parameters.AddWithValue("@BetBGameId", this.BetBGameId);
                command.Parameters.AddWithValue("@BetCGameId", this.BetCGameId);
                command.Parameters.AddWithValue("@BetDGameId", this.BetDGameId);
                command.Parameters.AddWithValue("@BetNote", this.BetNote);
                command.Parameters.AddWithValue("@SeasonId", this.SeasonId);
                command.Parameters.Add("@betId", SqlDbType.Int, 0, "betId");
                command.Parameters["@betId"].Direction = ParameterDirection.Output;

                if (this.BetADate == DateTime.MinValue)
                { command.Parameters["@BetADate"].Value = SqlDateTime.Null; }
                if (this.BetBDate == DateTime.MinValue)
                { command.Parameters["@BetBDate"].Value = SqlDateTime.Null; }
                if (this.BetCDate == DateTime.MinValue)
                { command.Parameters["@BetCDate"].Value = SqlDateTime.Null; }
                if (this.BetDDate == DateTime.MinValue)
                { command.Parameters["@BetDDate"].Value = SqlDateTime.Null; }

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    this.BetId = (int)command.Parameters["@betId"].Value;
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                    //throw (ex);
                }

            }

            if (this.BetId != 0)
            {
                foreach (BetStatus status in this.BetStatus)
                {
                    status.BetId = this.BetId;
                    status.InsertBetStatus();
                }
            }

            return true;
        }

        public bool TempUpdateBet()
        {
            Bet betCheck = new Bet();


            if (betCheck.BetId != 0)
            {
                this.BetId = betCheck.BetId;
            }
            string queryString = "";

            queryString = "UPDATE [dbo].[Bet]"
                                + "  SET [WinningBet] = @WinningBet"
                                    + " ,[BetNote] = @BetNote"
                               + " WHERE BetId = " + this.BetId.ToString()
                               + " SET @betId = " + this.BetId.ToString();

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                if (String.IsNullOrEmpty(this.BetNote)) { this.BetNote = ""; }
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@WinningBet", this.WinningBet);
                command.Parameters.AddWithValue("@BetNote", this.BetNote);
                command.Parameters.Add("@betId", SqlDbType.Int, 0, "betId");
                command.Parameters["@betId"].Direction = ParameterDirection.Output;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    this.BetId = (int)command.Parameters["@betId"].Value;
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex, this.BetId);
                }

            }

            return true;
        }
    }
}
