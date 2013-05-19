using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public class ListAccess
    {
        public List<Team> AllTeams(int leagueId)
        {
            List<Team> teams = new List<Team>();

            string queryString = "SELECT * FROM [dbo].[Teams] WHERE leagueId = @leagueId Order By City";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("leagueId", leagueId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Team team = new Team();
                        team.TeamId = reader.GetInt32(0);
                        team.LeagueId = reader.GetInt32(1);
                        team.League = reader.GetString(2);
                        team.ConferenceId = reader.GetInt32(3);
                        team.Conference = reader.GetString(4);
                        team.Name = reader.GetString(5);
                        team.City = reader.GetString(6);
                        team.ScrapId = reader.GetString(7);
                        team.ScrapUrl = reader.GetString(8);
                        team.Scrap2Url = reader.GetString(9);
                        teams.Add(team);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex, leagueId);
                }
            }

            return teams;
        }

        public List<Game> AllSeasonGames(int seasonId, int leagueId)
        {
            List<Game> games = new List<Game>();

            string queryString = "sp_FullSchedule";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@leagueId", leagueId);
                command.Parameters.AddWithValue("@seasonId", seasonId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Game game = new Game();

                        game.GameId = reader.GetInt32(0);
                        game.GameDate = reader.GetDateTime(1);
                        game.GameStatusTypeId = reader.GetInt32(2);
                        
                        game.HomeTeamId = reader.GetInt32(3);
                        game.HomeTeamConferenceId = reader.GetInt32(4);
                        game.HomeTeamName = reader.GetString(5);
                        game.HomeTeamScore = reader.GetInt32(6);
                        game.HomeTeamMoneyLine = reader.GetInt32(7);
                        game.HomeTeamSpread = reader.GetDecimal(8);
                        

                        game.AwayTeamId = reader.GetInt32(9);
                        game.AwayTeamConferenceId = reader.GetInt32(10);
                        game.AwayTeamName = reader.GetString(11);
                        game.AwayTeamScore = reader.GetInt32(12);
                        game.AwayTeamMoneyLine = reader.GetInt32(13);
                        game.AwayTeamSpread = reader.GetDecimal(14);

                        game.WinningTeamId = reader.GetInt32(15);
                        game.WinningTeamName = reader.GetString(16);

                        game.GameLocationName = reader.GetString(17);
                        game.SeasonId = reader.GetInt32(18);

                        
                        games.Add(game);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            return games;

        }

        public List<Game> MLBBetPotentials(int seasonId)
        {
            List<Game> games = new List<Game>();
            string queryString = "sp_MLBBetPotentials";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@seasonId", seasonId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Game game = new Game();

                        game.HomeTeamId = reader.GetInt32(0);
                        game.AwayTeamId = reader.GetInt32(1);                        
                        game.WinningTeamId = reader.GetInt32(2);

                        games.Add(game);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            int listLength = games.Count;
            Game tempGame;
            Game removeGame;
            for (int i = 0; i <= listLength; i++)
            {
                if (i <= games.Count)
                {
                    removeGame = null;
                    tempGame = games[i];
                    removeGame = games.Where(g => g.HomeTeamId == tempGame.AwayTeamId && g.AwayTeamId == tempGame.HomeTeamId).FirstOrDefault();
                    if (removeGame != null)
                    {
                        games.Remove(removeGame);
                    }
                }
                else
                {
                    break;
                }
            }

            return games;
        }

        public int[] NBABetGameIds()
        {
            List<int> gameIds = new List<int>();

            string queryString = "Select * From NBABets Order By AGameDate";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                //Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        gameIds.Add(reader.GetInt32(4));
                        gameIds.Add(reader.GetInt32(5));
                        gameIds.Add(reader.GetInt32(6));

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            return gameIds.Where(gId => gId != 0).Distinct().ToArray();
        }

        public List<Game> TeamSchedule(int seasonId, int teamId)
        {
            List<Game> games = new List<Game>();

            string queryString = "sp_TeamSchedule";
            using (SqlConnection connection = new SqlConnection(Base.conn))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@teamId", teamId);
                command.Parameters.AddWithValue("@seasonId", seasonId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Game game = new Game();

                        game.GameId = reader.GetInt32(0);
                        game.GameDate = reader.GetDateTime(1);
                        game.GameStatusTypeId = reader.GetInt32(2);

                        game.HomeTeamId = reader.GetInt32(3);
                       // game.HomeTeamConferenceId = reader.GetInt32(4);
                        game.HomeTeamName = reader.GetString(4);
                        game.HomeTeamScore = reader.GetInt32(5);

                        game.AwayTeamId = reader.GetInt32(13);
                        game.AwayTeamName = reader.GetString(14);
                        game.AwayTeamScore = reader.GetInt32(15);

                        game.WinningTeamId = reader.GetInt32(23);
                        game.WinningTeamName = reader.GetString(24);

                        //game.GameLocationName = reader.GetString(17);

                        if (teamId == game.HomeTeamId)
                        {
                            game.ScrapUrl = reader.GetString(12);
                        }
                        else
                        {
                            game.ScrapUrl = reader.GetString(22);
                        }

                        game.GameDateFormated = game.GameDate.ToShortDateString();

                        games.Add(game);

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            return games;
        }

        public List<Bet> GetSeasonBets(int seasonId, int leagueId){

            List<Bet> bets = new List<Bet>();

            
            return new List<Bet>();
        }

        public List<Bet> NBABets(int seasonId)
        {
            List<Bet> bets = new List<Bet>();
            string queryString = "SELECT * FROM [dbo].[NBABets]";
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
                        if (reader.GetInt32(3) == seasonId)
                        {
                            Bet bet = new Bet();
                            bet.BetId = reader.GetInt32(0);
                            bet.BetStatusText = reader.GetString(1);
                            bet.BetNote = reader.GetString(2);
                            bet.SeasonId = reader.GetInt32(3);
                            bet.BetAGameId = reader.GetInt32(4);
                            bet.BetBGameId = reader.GetInt32(5);
                            bet.BetCGameId = reader.GetInt32(6);
                            bet.BetDGameId = reader.GetInt32(60);
                            bet.BetADate = reader.GetDateSafe(7);

                            bet.BetGames = new List<Game>();

                            if (bet.BetAGameId != 0)
                            {
                                bet.BetGames.Add(new Game
                                {
                                    GameId = bet.BetAGameId,
                                    GameDate = reader.GetDateSafe(8),
                                    HomeTeamName = reader.GetString(9),
                                    HomeTeamScore = reader.GetInt32(10),
                                    AwayTeamName = reader.GetString(11),
                                    AwayTeamScore = reader.GetInt32(12),
                                    WinningTeamName = reader.GetString(13),
                                    HomeTeamMoneyLine = reader.GetInt32(14),
                                    HomeTeamSpread = reader.GetDecimalSafe(15),
                                    AwayTeamMoneyLine = reader.GetInt32(16),
                                    AwayTeamSpread = reader.GetDecimalSafe(17),
                                    WinningTeamId = reader.GetInt32(18),
                                    HomeTeamId = reader.GetInt32(19),
                                    AwayTeamId = reader.GetInt32(20),
                                    WonSpread = reader.GetBoolean(61),
                                    WonMoneyLine = reader.GetBoolean(62)
                                });
                                if (bet.BetGames.Last().WinningTeamId == bet.BetGames.Last().AwayTeamId)
                                {
                                    bet.WinA = true;
                                }
                                if (bet.BetGames.Last().WonSpread)
                                {
                                    bet.WinA = true;
                                }
                                if (!bet.BetGames.Last().WonSpread && !bet.BetGames.Last().WonMoneyLine)
                                {
                                    bet.WinA = false;
                                }
                            }

                            if (bet.BetBGameId != 0)
                            {
                                bet.BetGames.Add(new Game
                                {
                                    GameId = bet.BetBGameId,
                                    GameDate = reader.GetDateSafe(21),
                                    HomeTeamName = reader.GetString(22),
                                    HomeTeamScore = reader.GetInt32(23),
                                    AwayTeamName = reader.GetString(24),
                                    AwayTeamScore = reader.GetInt32(25),
                                    WinningTeamName = reader.GetString(26),
                                    HomeTeamMoneyLine = reader.GetInt32(27),
                                    HomeTeamSpread = reader.GetDecimalSafe(28),
                                    AwayTeamMoneyLine = reader.GetInt32(29),
                                    AwayTeamSpread = reader.GetDecimalSafe(30),
                                    WinningTeamId = reader.GetInt32(31),
                                    HomeTeamId = reader.GetInt32(32),
                                    AwayTeamId = reader.GetInt32(33),
                                    WonSpread = reader.GetBoolean(63),
                                    WonMoneyLine = reader.GetBoolean(64)
                                });
                                if (bet.BetGames.Last().WinningTeamId == bet.BetGames.Last().AwayTeamId)
                                {
                                    bet.WinB = true;
                                }
                                if (bet.BetGames.Last().WonSpread)
                                {
                                    bet.WinB = true;
                                }
                                if (!bet.BetGames.Last().WonSpread && !bet.BetGames.Last().WonMoneyLine)
                                {
                                    bet.WinB = false;
                                }
                            }


                            if (bet.BetCGameId != 0)
                            {
                                bet.BetGames.Add(new Game
                                {
                                    GameId = bet.BetCGameId,
                                    GameDate = reader.GetDateSafe(34),
                                    HomeTeamName = reader.GetString(35),
                                    HomeTeamScore = reader.GetInt32(36),
                                    AwayTeamName = reader.GetString(37),
                                    AwayTeamScore = reader.GetInt32(38),
                                    WinningTeamName = reader.GetString(39),
                                    HomeTeamMoneyLine = reader.GetInt32(40),
                                    HomeTeamSpread = reader.GetDecimalSafe(41),
                                    AwayTeamMoneyLine = reader.GetInt32(42),
                                    AwayTeamSpread = reader.GetDecimalSafe(43),
                                    WinningTeamId = reader.GetInt32(44),
                                    HomeTeamId = reader.GetInt32(45),
                                    AwayTeamId = reader.GetInt32(46),
                                    WonSpread = reader.GetBoolean(65),
                                    WonMoneyLine = reader.GetBoolean(66)
                                });
                                if (bet.BetGames.Last().WinningTeamId == bet.BetGames.Last().AwayTeamId)
                                {
                                    bet.WinC = true;
                                }
                                if (bet.BetGames.Last().WonSpread)
                                {
                                    bet.WinC = true;
                                }
                                if (!bet.BetGames.Last().WonSpread && !bet.BetGames.Last().WonMoneyLine)
                                {
                                    bet.WinC = false;
                                }
                            }

                            if (bet.BetDGameId != 0)
                            {
                                bet.BetGames.Add(new Game
                                {
                                    GameId = bet.BetDGameId,
                                    GameDate = reader.GetDateSafe(47),
                                    HomeTeamName = reader.GetString(48),
                                    HomeTeamScore = reader.GetInt32(49),
                                    AwayTeamName = reader.GetString(50),
                                    AwayTeamScore = reader.GetInt32(51),
                                    WinningTeamName = reader.GetString(52),
                                    HomeTeamMoneyLine = reader.GetInt32(53),
                                    HomeTeamSpread = reader.GetDecimalSafe(54),
                                    AwayTeamMoneyLine = reader.GetInt32(55),
                                    AwayTeamSpread = reader.GetDecimalSafe(56),
                                    WinningTeamId = reader.GetInt32(57),
                                    HomeTeamId = reader.GetInt32(58),
                                    AwayTeamId = reader.GetInt32(59),
                                    WonSpread = reader.GetBoolean(67),
                                    WonMoneyLine = reader.GetBoolean(68)
                                });
                                if (bet.BetGames.Last().WinningTeamId == bet.BetGames.Last().AwayTeamId)
                                {
                                    bet.WinD = true;
                                }
                                if (bet.BetGames.Last().WonSpread)
                                {
                                    bet.WinD = true;
                                }
                                if (!bet.BetGames.Last().WonSpread && !bet.BetGames.Last().WonMoneyLine)
                                {
                                    bet.WinD = false;
                                }
                            }

                            bets.Add(bet);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SportsError sError = new SportsError(ex);
                }
            }

            return bets.OrderBy(b => b.BetADate).ToList();

        }

    }
}
