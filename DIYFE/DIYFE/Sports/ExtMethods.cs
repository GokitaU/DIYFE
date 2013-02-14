using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Sports
{
    public static class ExtMethods
    {
        public static string TrimLastChar(this string str)
        {
            str = str.TrimEnd();
            return str.Substring(0, (str.Length - 1));
        }

        public static string RemoveInvalidChars(this string str)
        {
            str = str.Replace("|", "").Replace("&", "").Replace(" ", "");
            return str;
        }

        public static DateTime GetDateSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.MinValue;
        }

        public static bool HasPlayed(this DateTime gameDate)
        {
            if (gameDate.Date < DateTime.Now.Date)
            { return true; }
            else
            { return false; }
        }

        public static string GetStringSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public static int GetInt32Safe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return 0;
        }

        public static decimal GetDecimalSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDecimal(colIndex);
            else
                return 0;
        }

        //-- MONEYLINE WINS
        //--UPDATE [dbo].[Game]
        //--   SET [WinMoneyLine] = 1
        //--   WHERE WinningTeamId = AwayTeamId AND AwayTeamMoneyLine != 0

        //--UPDATE SPREAD WHEN AWAY TEAM IN NOT FAV AND THEY WON THE GAME
        //--UPDATE [MLB].[dbo].[Game]
        //--   SET [WinSpread] = 1
        //--WHERE AwayTeamSpread > 0 AND ([AwayTeamScore]-[HomeTeamScore]) > 0

        //--UPDATE SPREAD WHEN AWAY TEAM IS FAV AND THEY WON THE GAME
        //--UPDATE [dbo].[Game]
        //--   SET [WinSpread] = 1
        //--WHERE AwayTeamSpread < 0 AND WinningTeamId = AwayTeamId
        //--AND (AwayTeamSpread *-1) <= ([AwayTeamScore] - [HomeTeamScore])

        //--UPDATE SPREAD WHEN AWAY TEAM IS NOT FAV AND THEY LOSS THE GAME
        //--UPDATE [dbo].[Game]
        //--   SET [WinSpread] = 1
        //--WHERE AwayTeamSpread > 0 AND WinningTeamId != AwayTeamId
        //--AND (AwayTeamSpread) >= ([HomeTeamScore] - [AwayTeamScore])


        public static void MoneyLineBetsQuitWin(this List<Bet> bets)
        {
            decimal intBet = 500;
            decimal runningTotal = 500;
            decimal betAmount = intBet / 3;
            decimal moneyLineConvert = .01M;
            decimal spreadPayConvert = 1.1M;
            foreach (Bet bet in bets)
            {
                foreach (Game game in bet.BetGames)
                {
                    game.RunningTotal = runningTotal;

                    if (game.AwayTeamMoneyLine != 0)
                    {
                        if (game.WinningTeamId == game.AwayTeamId)
                        {
                            game.MoneyChange = betAmount * ((decimal)(game.AwayTeamMoneyLine) * moneyLineConvert);
                            bet.WinningBet = game.GameDate.ToShortDateString() + " WIN";
                            break;
                        }
                        else
                        {
                            game.MoneyChange = runningTotal - betAmount;
                        }
                        runningTotal = betAmount + game.MoneyChange;
                        betAmount = runningTotal / 3;
                    }
                    else if (game.AwayTeamSpread != 0)
                    {
                        if (game.WinningTeamId == game.AwayTeamId)
                        {

                            game.MoneyChange = betAmount * spreadPayConvert;
                            bet.WinningBet = game.GameDate.ToShortDateString() + " WIN";
                            break;
                        }
                        else
                        {
                            if ((game.AwayTeamScore - game.AwayTeamSpread) >= game.HomeTeamScore)
                            {
                                game.MoneyChange = betAmount * spreadPayConvert;
                                bet.WinningBet = game.GameDate.ToShortDateString() + " WIN";
                                break;
                            }
                        }
                        runningTotal = betAmount + game.MoneyChange;
                        betAmount = runningTotal / 3;
                    }
                }
                if (String.IsNullOrEmpty(bet.WinningBet))
                {
                    bet.WinningBet = "Loss";
                }
            }
        }

        public static void MoneyLineBetsAll(this List<Bet> bets)
        {
            decimal intBet = 500;
            decimal runningTotal = 500;
            decimal betAmount = intBet / 9;
            decimal moneyLineConvert = .01M;
            decimal spreadPayConvert = 1.1M;

            foreach (Bet bet in bets)
            {
                foreach (Game game in bet.BetGames)
                {
                    if (game.WinningTeamId == game.AwayTeamId)
                    {
                        bet.WinningBet = "Win";
                    }
                    if (game.AwayTeamMoneyLine != 0)
                    {
                        game.BetAmount = betAmount;
                        //This is a win
                        if (game.WinningTeamId == game.AwayTeamId)
                        {
                            game.MoneyChange = betAmount * game.AwayTeamMoneyLine * moneyLineConvert;
                            runningTotal += game.MoneyChange;
                            betAmount = runningTotal / 9;
                            bet.WinningBet = "Win";
                        }
                        else
                        {
                            game.MoneyChange = (betAmount * -1);
                            runningTotal += game.MoneyChange;
                        }

                    }
                    else
                    {
                        game.MoneyChange = (betAmount * -1);
                        runningTotal += game.MoneyChange;
                    }
                    game.RunningTotal = runningTotal;

                    if (runningTotal <= 0)
                    {
                        intBet = 500;
                        runningTotal = 500;
                        betAmount = intBet / 3;
                    }
                }
                if (String.IsNullOrEmpty(bet.WinningBet))
                {
                    bet.WinningBet = "Loss";
                }
            }
        }

        public static void SpreadBetsAll(this List<Bet> bets)
        {
            decimal intBet = 500;
            decimal runningTotal = 500;
            decimal betAmount = intBet / 9;
            decimal moneyLineConvert = .01M;
            decimal spreadPayConvert = 1.1M;

            int speadConvert = 0;
            int pointDiff = 0;
            int winIndex =-1;


            foreach (Bet bet in bets)
            {
                foreach (Game game in bet.BetGames)
                {
                    winIndex = -1;
                    speadConvert = (int)Math.Ceiling(game.AwayTeamSpread);
                    pointDiff = game.AwayTeamScore - game.HomeTeamScore;
                    
                    if (speadConvert < 0)
                    {
                        
                    }
                    else
                    {
                        if (pointDiff <= speadConvert)
                        {
                            winIndex = bet.BetGames.IndexOf(game);
                        }
                        else
                        {
                            winIndex = -1;
                        }
                    }

                    switch (winIndex)
                    {
                        case 0:
                            bet.WinA = true;
                            break;
                        case 1:
                            bet.WinB = true;
                            break;
                        case 2:
                            bet.WinC = true;
                            break;
                        case 3:
                            bet.WinD = true;
                            break;
                    }
                }
            }
        }

    }
}
