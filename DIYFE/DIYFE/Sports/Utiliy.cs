using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public class Utiliy
    {
        public Bet CheckGameBet(int gameId)
        {
            Bet bet = new Bet();
            string queryString = "SELECT Bet.[BetId],[BetADate],[BetBDate],[BetCDate],[BetDDate],[WinningBet],[WinningBetTeamId]" +
                                        ",[LosingBetTeamId],[BetNote] FROM [dbo].[Bet] INNER JOIN BetGame ON Bet.BetId = BetGame.BetId WHERE BetGame.GameId = " + gameId;

            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bet.BetId = reader.GetInt32(0);
                        bet.BetADate = reader.GetDateSafe(1);
                        bet.BetBDate = reader.GetDateSafe(2);
                        bet.BetCDate = reader.GetDateSafe(3);
                        bet.BetDDate = reader.GetDateSafe(4);
                        bet.WinningBet = reader.GetString(5);
                        bet.WinningBetTeamId = reader.GetInt32(6);
                        bet.LosingBetTeamId = reader.GetInt32(7);
                        bet.BetNote = reader.GetStringSafe(8);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }

            return bet;

        }

        //public List<Game> SeasonGames(int seasonId, int leagueId)
        //{

        //}

    }
}
