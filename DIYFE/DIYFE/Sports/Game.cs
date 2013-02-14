using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public class Game
    {

        public int GameId { get; set; }
        public int SeasonId { get; set; }
        public DateTime GameDate { get; set; }
        public string GameDateFormated { get; set; }
        public string GameLocationName { get; set; }
        public int GameStatusTypeId { get; set; }
        public string GameStatus { get; set; }

        public string HomeTeamName { get; set; }
        public int HomeTeamId { get; set; }
        public int HomeTeamScore { get; set; }
        public int HomeTeamConferenceId { get; set; }

        public decimal HomeTeamSpreadPay { get; set; }
        public decimal HomeTeamSpreadPayOpen { get; set; }
        public decimal HomeTeamSpread { get; set; }
        public decimal HomeTeamSpreadOpen { get; set; }
        public int HomeTeamMoneyLine { get; set; }
        public int HomeTeamMoneyLineOpen { get; set; }

        public string AwayTeamName { get; set; }
        public int AwayTeamId { get; set; }
        public int AwayTeamScore { get; set; }
        public int AwayTeamConferenceId { get; set; }

        public decimal AwayTeamSpreadPay { get; set; }
        public decimal AwayTeamSpreadPayOpen { get; set; }
        public decimal AwayTeamSpread { get; set; }
        public decimal AwayTeamSpreadOpen { get; set; }
        public int AwayTeamMoneyLine { get; set; }
        public int AwayTeamMoneyLineOpen { get; set; }

        public string WinningTeamName { get; set; }
        public int WinningTeamId { get; set; }

        public decimal MoneyChange { get; set; }
        public decimal RunningTotal { get; set; }
        public decimal BetAmount { get; set; }

        public bool WonMoneyLine { get; set; }
        public bool WonSpread { get; set; }
        public bool IsLoss { get; set; }

        public string ScrapUrl { get; set; }

        public Game() { }

        public Game(int gameId)
        {

            string queryString = "SELECT * FROM [dbo].[Games] WHERE GameId = @gameId";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@gameId", gameId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        this.GameId = reader.GetInt32(0);
                        this.GameDate = reader.GetDateTime(1);
                        this.GameStatusTypeId = reader.GetInt32(2);
                        this.HomeTeamId = reader.GetInt32(3);
                        this.HomeTeamName = reader.GetString(4);
                        this.HomeTeamScore = reader.GetInt32(5);
                        this.HomeTeamSpreadPay = reader.GetDecimalSafe(6);
                        this.HomeTeamSpreadPayOpen = reader.GetDecimalSafe(7);
                        this.HomeTeamSpread = reader.GetDecimalSafe(8);
                        this.HomeTeamSpreadOpen = reader.GetDecimalSafe(9);
                        this.HomeTeamMoneyLine = reader.GetInt32(10);
                        this.HomeTeamMoneyLineOpen = reader.GetInt32(11);
                        //this.HomeTeamLine = reader.GetDecimalSafe(6);
                        this.AwayTeamId = reader.GetInt32(13);
                        this.AwayTeamName = reader.GetString(14);
                        this.AwayTeamScore = reader.GetInt32(15);
                        this.AwayTeamSpreadPay = reader.GetDecimalSafe(16);
                        this.AwayTeamSpreadPayOpen = reader.GetDecimalSafe(17);
                        this.AwayTeamSpread = reader.GetDecimalSafe(18);
                        this.AwayTeamSpreadOpen = reader.GetDecimalSafe(19);
                        this.AwayTeamMoneyLine = reader.GetInt32(20);
                        this.AwayTeamMoneyLineOpen = reader.GetInt32(21);
                        //this.AwayTeamLine = reader.GetDecimalSafe(11);
                        this.WinningTeamId = reader.GetInt32(23);
                        this.WinningTeamName = reader.GetString(24);

                        this.SeasonId = reader.GetInt32(25);
                        this.GameLocationName = reader.GetString(26);

                        this.WonSpread = reader.GetBoolean(27);
                        this.WonMoneyLine = reader.GetBoolean(28);
                        

                    }
                    reader.Close();

                    this.GameDateFormated = this.GameDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex, gameId);
                }

            }
        }

        //private void _LoadGame() {
        //            string queryString = "SELECT * FROM [dbo].[Games] WHERE GameId = @gameId";

        //    using (SqlConnection connection = new SqlConnection(Base.conn))
        //    {
        //        // Create the Command and Parameter objects.
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        command.Parameters.AddWithValue("@gameId", this.GameId);
        //        try
        //        {
        //            connection.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                this.GameId = reader.GetInt32(0);
        //                this.GameDate = reader.GetDateTime(1);
        //                this.GameStatusTypeId = reader.GetInt32(2);
        //                this.HomeTeamId = reader.GetInt32(3);
        //                this.HomeTeamName = reader.GetString(4);
        //                this.HomeTeamScore = reader.GetInt32(5);
        //                this.HomeTeamSpreadPay = reader.GetDecimalSafe(6);
        //                this.HomeTeamSpreadPayOpen = reader.GetDecimalSafe(7);
        //                this.HomeTeamSpread = reader.GetDecimalSafe(8);
        //                this.HomeTeamSpreadOpen = reader.GetDecimalSafe(9);
        //                this.HomeTeamMoneyLine = reader.GetInt32(10);
        //                this.HomeTeamMoneyLineOpen = reader.GetInt32(11);
        //                //this.HomeTeamLine = reader.GetDecimalSafe(6);
        //                this.AwayTeamId = reader.GetInt32(13);
        //                this.AwayTeamName = reader.GetString(14);
        //                this.AwayTeamScore = reader.GetInt32(15);
        //                this.AwayTeamSpreadPay = reader.GetDecimalSafe(16);
        //                this.AwayTeamSpreadPayOpen = reader.GetDecimalSafe(17);
        //                this.AwayTeamSpread = reader.GetDecimalSafe(18);
        //                this.AwayTeamSpreadOpen = reader.GetDecimalSafe(19);
        //                this.AwayTeamMoneyLine = reader.GetInt32(20);
        //                this.AwayTeamMoneyLineOpen = reader.GetInt32(21);
        //                //this.AwayTeamLine = reader.GetDecimalSafe(11);
        //                this.WinningTeamId = reader.GetInt32(23);
        //                this.WinningTeamName = reader.GetString(24);

        //                this.SeasonId = reader.GetInt32(25);
        //                this.GameLocationName = reader.GetString(26);

        //                this.WonSpread = reader.GetBoolean(27);
        //                this.WonMoneyLine = reader.GetBoolean(28);
                        

        //            }
        //            reader.Close();

        //            this.GameDateFormated = this.GameDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
        //        }
        //        catch (Exception ex)
        //        {
        //            SportsError sError = new SportsError(ex, gameId);
        //        }

        //    }
        //}

        public bool InsertGame()
        {
            Game gameCheck = new Game();
            string queryCheckGame = "SELECT [GameId],[SeasonId],[GameDate],[GameStatusTypeId] " +
                                        ",[HomeTeamId],[HomeTeamScore] " +
                                        ",[AwayTeamId],[AwayTeamScore] " +
                                        ",[WinningTeamId] FROM [dbo].[Game] "
                                    + "   WHERE HomeTeamId = @HomeTeamId "
                                    + " AND AwayTeamId = @AwayTeamId "
                                    + " AND HomeTeamScore = @HomeTeamScore "
                                    + " AND AwayTeamScore = @AwayTeamScore "
                                    + " AND SeasonId = @SeasonId AND GameDate = @GameDate";

            //if (this.HomeTeamId == 31 && this.AwayTeamId == 39)
            //{
            //    bool test = true;
            //}
            //if (this.HomeTeamId == 39 && this.AwayTeamId == 31)
            //{
            //    bool test = true;
            //}
            using (SqlConnection connectionCheck = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand commandCheck = new SqlCommand(queryCheckGame, connectionCheck);
                commandCheck.Parameters.AddWithValue("@HomeTeamId", this.HomeTeamId);
                commandCheck.Parameters.AddWithValue("@AwayTeamId", this.AwayTeamId);
                commandCheck.Parameters.AddWithValue("@HomeTeamScore", this.HomeTeamScore);
                commandCheck.Parameters.AddWithValue("@AwayTeamScore", this.AwayTeamScore);
                commandCheck.Parameters.AddWithValue("@GameDate", this.GameDate);
                commandCheck.Parameters.AddWithValue("@SeasonId", this.SeasonId);

                //TEMP COMMENT OUT TO INSERT TWO GAMES ON SAME DAY!

                try
                {
                    connectionCheck.Open();
                    SqlDataReader reader = commandCheck.ExecuteReader();
                    while (reader.Read())
                    {
                        gameCheck.GameId = reader.GetInt32(0);
                        gameCheck.SeasonId = reader.GetInt32(1);
                        gameCheck.GameDate = reader.GetDateTime(2);
                        gameCheck.GameStatusTypeId = reader.GetInt32(3);

                        gameCheck.HomeTeamId = reader.GetInt32(4);
                        gameCheck.HomeTeamScore = reader.GetInt32(5);
                        // gameCheck.HomeTeamLine = 0;  //6

                        gameCheck.AwayTeamId = reader.GetInt32(6);
                        gameCheck.AwayTeamScore = reader.GetInt32(7);
                        //  gameCheck.AwayTeamLine = 0; //9

                        gameCheck.WinningTeamId = reader.GetInt32(8);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            if (gameCheck.GameId != 0)
            {
                //if (gameCheck.GameStatusTypeId != this.GameStatusTypeId && gameCheck.WinningTeamId != this.WinningTeamId)
                //{
                //    gameCheck.GameStatusTypeId = this.GameStatusTypeId;
                //    gameCheck.WinningTeamId = this.WinningTeamId;
                //    gameCheck.HomeTeamScore = this.HomeTeamScore;
                //    gameCheck.AwayTeamScore = this.AwayTeamScore;
                //    gameCheck.UpdateGame();
                //}
            }
            else
            {

                string queryString = "INSERT INTO [dbo].[Game] "
                                           + "([SeasonId],[GameDate],[GameStatusTypeId],[HomeTeamId],[HomeTeamScore] "
                                           + ",[AwayTeamId],[AwayTeamScore],[WinningTeamId]) "
                                     + " VALUES "
                                           + "(@SeasonId,@GameDate,@GameStatusTypeId,@HomeTeamId,@HomeTeamScore "
                                           + ",@AwayTeamId,@AwayTeamScore,@WinningTeamId)";

                using (SqlConnection connection = new SqlConnection(Base.conn))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@SeasonId", this.SeasonId);
                    command.Parameters.AddWithValue("@GameDate", this.GameDate);
                    command.Parameters.AddWithValue("@GameStatusTypeID", this.GameStatusTypeId);
                    command.Parameters.AddWithValue("@HomeTeamId", this.HomeTeamId);
                    command.Parameters.AddWithValue("@HomeTeamScore", this.HomeTeamScore);
                    //command.Parameters.AddWithValue("@HomeTeamLine", this.HomeTeamLine);
                    command.Parameters.AddWithValue("@AwayTeamId", this.AwayTeamId);
                    command.Parameters.AddWithValue("@AwayTeamScore", this.AwayTeamScore);
                   // command.Parameters.AddWithValue("@AwayTeamLine", this.AwayTeamLine);
                    command.Parameters.AddWithValue("@WinningTeamId", this.WinningTeamId);

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
            }

            return true;
        }

        public bool UpdateGame()
        {
            if (this.GameId != 0)
            {
                string queryString = "UPDATE [dbo].[Game] "
                                       + "SET [GameStatusTypeId] = @GameStatusTypeId "
                                          + " ,[HomeTeamScore] = @HomeTeamScore"
                                          + " ,[AwayTeamScore] = @AwayTeamScore"
                                          + " ,[WinningTeamId] = @WinningTeamId"
                                     + " WHERE GameId = @GameId";

                using (SqlConnection connection = new SqlConnection(Base.conn))
                {
                    // Create the Command and Parameter objects.
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@GameId", this.GameId);
                    //command.Parameters.AddWithValue("@GameDate", this.GameDate);
                    command.Parameters.AddWithValue("@GameStatusTypeId", this.GameStatusTypeId);
                    command.Parameters.AddWithValue("@HomeTeamScore", this.HomeTeamScore);
                    command.Parameters.AddWithValue("@AwayTeamScore", this.AwayTeamScore);
                    command.Parameters.AddWithValue("@WinningTeamId", this.WinningTeamId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        SportsError sError = new SportsError(ex, this.GameId);

                    }
                }


                Utiliy u = new Utiliy();
                Bet bet = u.CheckGameBet(this.GameId);
                if (bet.BetId != 0)
                {
                    //This is a own bet
                    if (bet.WinningBetTeamId == this.WinningTeamId)
                    {
                        if (this.GameDate == bet.BetADate)
                        {
                            bet.WinningBet = "A";
                        }
                        if (this.GameDate == bet.BetBDate)
                        {
                            bet.WinningBet = "B";
                        }
                        if (this.GameDate == bet.BetCDate)
                        {
                            bet.WinningBet = "C";
                        }
                        if (this.GameDate == bet.BetDDate)
                        {
                            bet.WinningBet = "D";
                        }

                        bet.TempUpdateBet();
                    }
                }

            }
            return true;
        }

        public bool UpdateGameLine()
        {
            string queryString = "UPDATE [dbo].[Game] "
                                       + "SET [HomeTeamSpreadPay] = @HomeTeamSpreadPay "
                                          + ",[HomeTeamSpreadPayOpen] = @HomeTeamSpreadPayOpen "
                                          + ",[HomeTeamScore] = @HomeTeamScore "
                                          + ",[HomeTeamSpread] = @HomeTeamSpread "
                                          + ",[HomeTeamSpreadOpen] = @HomeTeamSpreadOpen "
                                          + ",[HomeTeamMoneyLine] = @HomeTeamMoneyLine "
                                          + ",[HomeTeamMoneyLineOpen] = @HomeTeamMoneyLineOpen "
                                          + ",[AwayTeamSpreadPay] = @AwayTeamSpreadPay "
                                          + ",[AwayTeamScore] = @AwayTeamScore "
                                          + ",[AwayTeamSpreadPayOpen] = @AwayTeamSpreadPayOpen "
                                          + ",[AwayTeamSpread] = @AwayTeamSpread "
                                          + ",[AwayTeamSpreadOpen] = @AwayTeamSpreadOpen "
                                          + ",[AwayTeamMoneyLine] = @AwayTeamMoneyLine "
                                          + ",[AwayTeamMoneyLineOpen] = @AwayTeamMoneyLineOpen "
                                          + ",[WinSpread] = @WinSpread "
                                          + ",[WinMoneyLine] = @WinMoneyLine ";
            if (this.AwayTeamScore > 0 && this.HomeTeamScore > 0)
            {
                queryString = queryString + ",[GameStatusTypeId] = @GameStatusTypeId ";
                queryString = queryString + ",[WinningTeamId] = @WinningTeamId ";
                if (this.AwayTeamScore > this.HomeTeamScore)
                {
                    this.WinningTeamId = this.AwayTeamId;
                }else{
                    this.WinningTeamId = this.HomeTeamId;
                }
            }
            queryString = queryString +" WHERE GameId = @GameId";

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@GameId", this.GameId);
                command.Parameters.AddWithValue("@GameStatusTypeId", 1);
                command.Parameters.AddWithValue("@WinningTeamId", this.WinningTeamId);
                command.Parameters.AddWithValue("@HomeTeamScore", this.HomeTeamScore);
                command.Parameters.AddWithValue("@AwayTeamScore", this.AwayTeamScore);
                command.Parameters.AddWithValue("@HomeTeamSpreadPay", this.HomeTeamSpreadPay);
                command.Parameters.AddWithValue("@HomeTeamSpreadPayOpen", this.HomeTeamSpreadPayOpen);
                command.Parameters.AddWithValue("@HomeTeamSpread", this.HomeTeamSpread);
                command.Parameters.AddWithValue("@HomeTeamSpreadOpen", this.HomeTeamSpreadOpen);
                command.Parameters.AddWithValue("@HomeTeamMoneyLine", this.HomeTeamMoneyLine);
                command.Parameters.AddWithValue("@HomeTeamMoneyLineOpen", this.HomeTeamMoneyLineOpen);
                command.Parameters.AddWithValue("@AwayTeamSpreadPay", this.AwayTeamSpreadPay);
                command.Parameters.AddWithValue("@AwayTeamSpreadPayOpen", this.AwayTeamSpreadPayOpen);
                command.Parameters.AddWithValue("@AwayTeamSpread", this.AwayTeamSpread);
                command.Parameters.AddWithValue("@AwayTeamSpreadOpen", this.AwayTeamSpreadOpen);
                command.Parameters.AddWithValue("@AwayTeamMoneyLine", this.AwayTeamMoneyLine);
                command.Parameters.AddWithValue("@AwayTeamMoneyLineOpen", this.AwayTeamMoneyLineOpen);
                command.Parameters.AddWithValue("@WinSpread", this.WonSpread);
                command.Parameters.AddWithValue("@WinMoneyLine", this.WonMoneyLine);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex, this.GameId);

                }
            }
            return true;
        }
    }
}
