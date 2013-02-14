using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using DIYFE.Web.Models.Sports;
using Sports;

namespace DIYFE.Web
{
    public static class ExtMethods
    {
        //public static string ToJson(this Object obj)
        //{
        //    return new JavaScriptSerializer().Serialize(obj);
        //}

        //public static string Year(this DateTime? dt)
        //{
        //    return "1900";
        //}

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

        public static void VersionThree(this SeasonBets sBets, AppStatic.Seasons season, AppStatic.BettingSetup bettingSet){
            
            ListAccess la = new ListAccess();
            List<Game> seasonGames = la.AllSeasonGames((int)season, (int)AppStatic.Leagues.NBA);
            List<Game> teamGames;
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();
            bool hasWon = false;
            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;
            int gameCount = 0;
            foreach (Team team in AppStatic.NBATeams)
            {
                teamGames = seasonGames.Where(sg => sg.AwayTeamId == team.TeamId || sg.HomeTeamId == team.TeamId).OrderBy(sg => sg.GameDate).ToList();
                for (int i = 0; i < teamGames.Count(); i++)
                {
                    while (i < teamGames.Count() && teamGames[i].AwayTeamId == team.TeamId)// && bettingSeries.Count <= 3)
                    {
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        bettingSeries.Add(teamGames[i]);
                        i++;
                    }

                    if (bettingSeries.Count >= 3)
                    {
                         //i--;
                         Bet bet = new Bet();
                         bet.BetGames = new List<Game>();
                         bet.BetGames.AddRange(bettingSeries);
                         bet.BetADate = bettingSeries[0].GameDate;
                        foreach (Game game in bettingSeries)
                        {
                            if (game.AwayTeamId == game.WinningTeamId)
                            {
                                if (game.AwayTeamSpread < 0)
                                {
                                    int dif = game.HomeTeamScore - game.AwayTeamScore;
                                    if (dif <= game.AwayTeamSpread)
                                    {
                                        hasWon = true;
                                    }
                                }
                                else
                                {
                                    hasWon = true;
                                }
                            }
                            else
                            {
                                if (game.AwayTeamSpread > 0 && (game.HomeTeamScore - game.AwayTeamScore) <= game.AwayTeamSpread && game.WinningTeamId != 0)
                                { hasWon = true;  }
                            }

                            if (hasWon)
                            {
                                int winIndex = bettingSeries.IndexOf(game);
                                switch (winIndex)
                                {
                                    case 0:
                                        bet.WinA = true;
                                        sBets.TotalA++;
                                        break;
                                    case 1:
                                        bet.WinB = true;
                                        bet.BetBDate = game.GameDate;
                                        sBets.TotalB++;
                                        break;
                                    case 2:
                                        bet.WinC = true;
                                        bet.BetBDate = bettingSeries[1].GameDate;
                                        bet.BetCDate = game.GameDate;
                                        sBets.TotalC++;
                                        break;
                                    case 3:
                                        bet.WinD = true;
                                        bet.BetBDate = bettingSeries[1].GameDate;
                                        bet.BetCDate = bettingSeries[2].GameDate;
                                        bet.BetDDate = game.GameDate;
                                        sBets.TotalD++;
                                        break;
                                }
                                break;
                            }
                            else
                            {
                                //bet.Loss = true;
                                //bet.LossDate = game.GameDate;
                            }
                        }
                        if (!hasWon)
                        {
                            bet.Loss = true;
                            bet.LossDate = bettingSeries.Last().GameDate;
                            sBets.TotalGroupLoss++;
                        }
                        sBets.Bets.Add(bet);
                        hasWon = false;
                        bettingSeries.Clear();

                        #region
                        //switch (bettingSet)
                        //{
                        //    //case AppStatic.BettingSetup.MoneyLine:
                        //    //    foreach (Game game in bettingSeries)
                        //    //    {
                        //    //        if (game.AwayTeamId == game.WinningTeamId)
                        //    //        {
                                        
                        //    //        }
                        //    //    }

                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread:
                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread3Point:
                        //    //    break;

                        //}
                        #endregion

                        #region
                        //if (bettingSeries.Count >= 2)
                        //{
                        //    Bet bet = new Bet();
                        //    bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
                        //    bet.SeasonId = bettingSeries[0].SeasonId;
                        //    bet.BetStatus = new List<BetStatus>();
                        //    foreach (Game game in bettingSeries)
                        //    {
                        //        switch (bettingSeries.IndexOf(game))
                        //        {
                        //            case 0:
                        //                bet.BetADate = game.GameDate;
                        //                bet.BetAGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "A";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 1 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 1:
                        //                bet.BetBDate = game.GameDate;
                        //                bet.BetBGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "B";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
                        //                    //bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 2:
                        //                bet.BetCDate = game.GameDate;
                        //                bet.BetCGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 3:
                        //                bet.BetDDate = game.GameDate;
                        //                bet.BetDGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 7 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //        }
                        //    }
                        //    if (bet.BetStatus.Count() == 0)
                        //    {
                        //        if (bet.BetCDate.HasPlayed())
                        //        {
                        //            bet.WinningBet = "Loss";
                        //            bet.LosingBetTeamId = bet.WinningBetTeamId;
                        //            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
                        //        }
                        //    }

                        //    bet.InsertBet();

                            // gameCount = 0;
                        //}

                        #endregion
                    }
                    else
                    {
                        bettingSeries.Clear();
                    }
     
                }
                teamGames.Clear();
            }
           sBets.Bets = sBets.Bets.OrderBy(b => b.BetADate).ToList();
           //TODAY'S BETS
           sBets.TodaysBets = sBets.Bets.SelectMany(g => g.BetGames)
               .Where(g => g.GameDate.ToShortDateString() == DateTime.Today.ToShortDateString() && g.GameStatusTypeId==1).ToList();
           sBets.TomorrowBets = sBets.Bets.SelectMany(g => g.BetGames).Where(g => g.GameDate.ToShortDateString() == DateTime.Today.AddDays(1).ToShortDateString() && g.GameStatusTypeId == 1).ToList();

           int bankOne = 800;
           int bankOneWinAmount = 100;
           int bankOneIndex = 1;

           int bankTwo = 800;
           int bankTwoWinAmount = 100;
           int bankTwoIndex = 1;

           int bankThree = 800;
           int bankThreeWinAmount = 100;
           int bankThreeIndex = 1;

           int bankBetIndex = 1;
           
           int tempA = 0;
           int tempB = 0;
           int tempC = 0;

           foreach(Bet bet in sBets.Bets)
           {
               if (bankBetIndex == 3)
               { bankBetIndex = 1; }
               else
               {  bankBetIndex++; }

               if (!bet.Loss)
               {
                   switch (bankBetIndex)
                   {
                       case 1:
                           bankOne = bankOne + bankOneWinAmount;
                           tempC = (int)(Math.Round(bankOne * .1) / 2);
                           tempB = (int)(Math.Round(tempC * .1) / 2);
                           tempA = (int)(Math.Round(tempB * .1) / 2);

                           break;
                       case 2:
                           bankTwo = bankTwo + bankTwoWinAmount;
                           
                           break;
                       case 3:
                           bankThree = bankThree + bankThreeWinAmount;
                           
                           break;
                   }
               }

            }

            //List<Game> games = la.AllSeasonGames((int)AppStatic.Seasons._2011, (int)AppStatic.Leagues.NBA);
            //int[] teamIds = games.Select(g => g.AwayTeamId);

        }

        public static void VersionFour(this SeasonBets sBets, AppStatic.Seasons season, AppStatic.BettingSetup bettingSet)
        {

            ListAccess la = new ListAccess();
            List<Game> seasonGames = la.AllSeasonGames((int)season, (int)AppStatic.Leagues.NBA);
            List<Game> teamGames;
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();
            bool hasWon = false;
            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;
            int gameCount = 0;
            foreach (Team team in AppStatic.NBATeams)
            {
                teamGames = seasonGames.Where(sg => sg.AwayTeamId == team.TeamId || sg.HomeTeamId == team.TeamId).OrderBy(sg => sg.GameDate).ToList();
                for (int i = 0; i < teamGames.Count(); i++)
                {
                    while (i < teamGames.Count() && teamGames[i].AwayTeamId == team.TeamId)// && bettingSeries.Count <= 3)
                    {
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        bettingSeries.Add(teamGames[i]);
                        i++;
                    }

                    if (bettingSeries.Count >= 3)
                    {
                        //i--;
                        Bet bet = new Bet();
                        bet.BetGames = new List<Game>();
                        bet.BetGames.AddRange(bettingSeries);
                        bet.BetADate = bettingSeries[0].GameDate;
                        foreach (Game game in bettingSeries)
                        {
                            if (game.AwayTeamId == game.WinningTeamId)
                            {
                                if (game.AwayTeamSpread < 0)
                                {
                                    int dif = game.HomeTeamScore - game.AwayTeamScore;
                                    if (dif <= game.AwayTeamSpread)
                                    {
                                        hasWon = true;
                                    }
                                }
                                else
                                {
                                    hasWon = true;
                                }
                            }
                            else
                            {
                                if (game.AwayTeamSpread > 0 && (game.HomeTeamScore - game.AwayTeamScore) <= game.AwayTeamSpread && game.WinningTeamId != 0)
                                { hasWon = true; }
                            }

                            if (hasWon)
                            {
                                int winIndex = bettingSeries.IndexOf(game);
                                switch (winIndex)
                                {
                                    case 0:
                                        bet.WinA = true;
                                        sBets.TotalA++;
                                        break;
                                    case 1:
                                        bet.WinB = true;
                                        sBets.TotalB++;
                                        break;
                                    case 2:
                                        bet.WinC = true;
                                        sBets.TotalC++;
                                        break;
                                    case 3:
                                        bet.WinD = true;
                                        sBets.TotalD++;
                                        break;
                                }
                                break;
                            }
                            else
                            {
                                bet.Loss = true;
                            }
                        }
                        if (!hasWon)
                        {
                            sBets.TotalGroupLoss++;
                        }
                        sBets.Bets.Add(bet);
                        hasWon = false;
                        bettingSeries.Clear();

                        #region
                        //switch (bettingSet)
                        //{
                        //    //case AppStatic.BettingSetup.MoneyLine:
                        //    //    foreach (Game game in bettingSeries)
                        //    //    {
                        //    //        if (game.AwayTeamId == game.WinningTeamId)
                        //    //        {

                        //    //        }
                        //    //    }

                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread:
                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread3Point:
                        //    //    break;

                        //}
                        #endregion

                        #region
                        //if (bettingSeries.Count >= 2)
                        //{
                        //    Bet bet = new Bet();
                        //    bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
                        //    bet.SeasonId = bettingSeries[0].SeasonId;
                        //    bet.BetStatus = new List<BetStatus>();
                        //    foreach (Game game in bettingSeries)
                        //    {
                        //        switch (bettingSeries.IndexOf(game))
                        //        {
                        //            case 0:
                        //                bet.BetADate = game.GameDate;
                        //                bet.BetAGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "A";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 1 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 1:
                        //                bet.BetBDate = game.GameDate;
                        //                bet.BetBGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "B";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
                        //                    //bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 2:
                        //                bet.BetCDate = game.GameDate;
                        //                bet.BetCGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 3:
                        //                bet.BetDDate = game.GameDate;
                        //                bet.BetDGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 7 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //        }
                        //    }
                        //    if (bet.BetStatus.Count() == 0)
                        //    {
                        //        if (bet.BetCDate.HasPlayed())
                        //        {
                        //            bet.WinningBet = "Loss";
                        //            bet.LosingBetTeamId = bet.WinningBetTeamId;
                        //            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
                        //        }
                        //    }

                        //    bet.InsertBet();

                        // gameCount = 0;
                        //}

                        #endregion
                    }
                    else
                    {
                        bettingSeries.Clear();
                    }

                }
                teamGames.Clear();
            }
            sBets.Bets = sBets.Bets.OrderBy(b => b.BetADate).ToList();
            //TODAY'S BETS
            sBets.TodaysBets = sBets.Bets.SelectMany(g => g.BetGames)
                .Where(g => g.GameDate.ToShortDateString() == DateTime.Today.ToShortDateString() && g.GameStatusTypeId == 1).ToList();
            sBets.TomorrowBets = sBets.Bets.SelectMany(g => g.BetGames).Where(g => g.GameDate.ToShortDateString() == DateTime.Today.AddDays(1).ToShortDateString() && g.GameStatusTypeId == 1).ToList();

            int bankOne = 800;
            int bankOneWinAmount = 100;
            int bankOneIndex = 1;

            int bankTwo = 800;
            int bankTwoWinAmount = 100;
            int bankTwoIndex = 1;

            int bankThree = 800;
            int bankThreeWinAmount = 100;
            int bankThreeIndex = 1;

            int bankBetIndex = 1;

            int tempA = 0;
            int tempB = 0;
            int tempC = 0;

            foreach (Bet bet in sBets.Bets)
            {
                if (bankBetIndex == 3)
                { bankBetIndex = 1; }
                else
                { bankBetIndex++; }

                if (!bet.Loss)
                {
                    switch (bankBetIndex)
                    {
                        case 1:
                            bankOne = bankOne + bankOneWinAmount;
                            tempC = (int)(Math.Round(bankOne * .1) / 2);
                            tempB = (int)(Math.Round(tempC * .1) / 2);
                            tempA = (int)(Math.Round(tempB * .1) / 2);

                            break;
                        case 2:
                            bankTwo = bankTwo + bankTwoWinAmount;

                            break;
                        case 3:
                            bankThree = bankThree + bankThreeWinAmount;

                            break;
                    }
                }

            }

            //List<Game> games = la.AllSeasonGames((int)AppStatic.Seasons._2011, (int)AppStatic.Leagues.NBA);
            //int[] teamIds = games.Select(g => g.AwayTeamId);

        }

        public static void ThreeBank(this SeasonBets sBets)
        {
            int bankIndex = 0;
            foreach (Bet bet in sBets.Bets)
            {
                if (bankIndex == 3)
                { bankIndex = 0; }

                switch (bankIndex)
                {
                    case 0:

                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                }
                bankIndex++;
            }
        }


        public static void ExcelFormat(this SeasonBets sBets, AppStatic.Seasons season, AppStatic.BettingSetup bettingSet)
        {

            ListAccess la = new ListAccess();
            List<Game> seasonGames = la.AllSeasonGames((int)season, (int)AppStatic.Leagues.NBA);
            List<Game> teamGames;
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();
            bool hasWon = false;
            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;
            int gameCount = 0;
            foreach (Team team in AppStatic.NBATeams)
            {
                teamGames = seasonGames.Where(sg => sg.AwayTeamId == team.TeamId || sg.HomeTeamId == team.TeamId).OrderBy(sg => sg.GameDate).ToList();
                for (int i = 0; i < teamGames.Count(); i++)
                {
                    while (i < teamGames.Count() && teamGames[i].AwayTeamId == team.TeamId)// && bettingSeries.Count <= 3)
                    {
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        if (teamGames[i].GameId == 105651)
                        {
                            var testOne = true;
                        }
                        bettingSeries.Add(teamGames[i]);
                        i++;
                    }

                    if (bettingSeries.Count >= 3)
                    {
                        //i--;
                        Bet bet = new Bet();
                        bet.BetGames = new List<Game>();
                        bet.BetGames.AddRange(bettingSeries);
                        bet.BetADate = bettingSeries[0].GameDate;
                        foreach (Game game in bettingSeries)
                        {
                            if (game.AwayTeamId == game.WinningTeamId)
                            {
                                if (game.AwayTeamSpread < 0)
                                {
                                    int dif = game.HomeTeamScore - game.AwayTeamScore;
                                    if (dif <= game.AwayTeamSpread)
                                    {
                                        hasWon = true;
                                    }
                                }
                                else
                                {
                                    hasWon = true;
                                }
                            }
                            else
                            {
                                if (game.AwayTeamSpread > 0 && (game.HomeTeamScore - game.AwayTeamScore) <= game.AwayTeamSpread && game.WinningTeamId != 0)
                                { hasWon = true; }
                            }

                            if (hasWon)
                            {
                                int winIndex = bettingSeries.IndexOf(game);
                                switch (winIndex)
                                {
                                    case 0:
                                        bet.WinA = true;
                                        sBets.TotalA++;
                                        break;
                                    case 1:
                                        bet.WinB = true;
                                        sBets.TotalB++;
                                        break;
                                    case 2:
                                        bet.WinC = true;
                                        sBets.TotalC++;
                                        break;
                                    case 3:
                                        bet.WinD = true;
                                        sBets.TotalD++;
                                        break;
                                }
                                break;
                            }
                            else
                            {
                                bet.Loss = true;
                            }
                        }
                        if (!hasWon)
                        {
                            sBets.TotalGroupLoss++;
                        }
                        bet.BetId = sBets.Bets.Count + 1;
                        sBets.Bets.Add(bet);
                        hasWon = false;
                        bettingSeries.Clear();

                        #region
                        //switch (bettingSet)
                        //{
                        //    //case AppStatic.BettingSetup.MoneyLine:
                        //    //    foreach (Game game in bettingSeries)
                        //    //    {
                        //    //        if (game.AwayTeamId == game.WinningTeamId)
                        //    //        {

                        //    //        }
                        //    //    }

                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread:
                        //    //    break;
                        //    //case AppStatic.BettingSetup.Spread3Point:
                        //    //    break;

                        //}
                        #endregion

                        #region
                        //if (bettingSeries.Count >= 2)
                        //{
                        //    Bet bet = new Bet();
                        //    bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
                        //    bet.SeasonId = bettingSeries[0].SeasonId;
                        //    bet.BetStatus = new List<BetStatus>();
                        //    foreach (Game game in bettingSeries)
                        //    {
                        //        switch (bettingSeries.IndexOf(game))
                        //        {
                        //            case 0:
                        //                bet.BetADate = game.GameDate;
                        //                bet.BetAGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "A";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 1 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 1:
                        //                bet.BetBDate = game.GameDate;
                        //                bet.BetBGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "B";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
                        //                    //bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 2:
                        //                bet.BetCDate = game.GameDate;
                        //                bet.BetCGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //            case 3:
                        //                bet.BetDDate = game.GameDate;
                        //                bet.BetDGameId = game.GameId;
                        //                if (game.WinningTeamId == game.AwayTeamId)
                        //                {
                        //                    //bet.WinningBet = "C";
                        //                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 7 });
                        //                    // bet.LosingBetTeamId = game.HomeTeamId;
                        //                }
                        //                break;
                        //        }
                        //    }
                        //    if (bet.BetStatus.Count() == 0)
                        //    {
                        //        if (bet.BetCDate.HasPlayed())
                        //        {
                        //            bet.WinningBet = "Loss";
                        //            bet.LosingBetTeamId = bet.WinningBetTeamId;
                        //            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
                        //        }
                        //    }

                        //    bet.InsertBet();

                        // gameCount = 0;
                        //}

                        #endregion
                    }
                    else
                    {
                        bettingSeries.Clear();
                    }

                }
                teamGames.Clear();
            }
            sBets.Bets = sBets.Bets.OrderBy(b => b.BetADate).ToList();
            //TODAY'S BETS
            sBets.TodaysBets = sBets.Bets.SelectMany(g => g.BetGames)
                .Where(g => g.GameDate.ToShortDateString() == DateTime.Today.ToShortDateString() && g.GameStatusTypeId == 1).ToList();
            sBets.TomorrowBets = sBets.Bets.SelectMany(g => g.BetGames).Where(g => g.GameDate.ToShortDateString() == DateTime.Today.AddDays(1).ToShortDateString() && g.GameStatusTypeId == 1).ToList();

            int bankOne = 800;
            int bankOneWinAmount = 100;
            int bankOneIndex = 1;

            int bankTwo = 800;
            int bankTwoWinAmount = 100;
            int bankTwoIndex = 1;

            int bankThree = 800;
            int bankThreeWinAmount = 100;
            int bankThreeIndex = 1;

            int bankBetIndex = 1;

            int tempA = 0;
            int tempB = 0;
            int tempC = 0;

            foreach (Bet bet in sBets.Bets)
            {
                if (bankBetIndex == 3)
                { bankBetIndex = 1; }
                else
                { bankBetIndex++; }

                if (!bet.Loss)
                {
                    switch (bankBetIndex)
                    {
                        case 1:
                            bankOne = bankOne + bankOneWinAmount;
                            tempC = (int)(Math.Round(bankOne * .1) / 2);
                            tempB = (int)(Math.Round(tempC * .1) / 2);
                            tempA = (int)(Math.Round(tempB * .1) / 2);

                            break;
                        case 2:
                            bankTwo = bankTwo + bankTwoWinAmount;

                            break;
                        case 3:
                            bankThree = bankThree + bankThreeWinAmount;

                            break;
                    }
                }

            }

            //List<Game> games = la.AllSeasonGames((int)AppStatic.Seasons._2011, (int)AppStatic.Leagues.NBA);
            //int[] teamIds = games.Select(g => g.AwayTeamId);

        }

        public static void BankRoll(this ExcelModel model, int dumpAt, int dumpAmount, int aBet, int bBet, int cBet, int dBet){
            
            model.BetList = model.BetList.OrderBy(e => e.BetDate).ThenByDescending(e => e.Winning).ToList();

            int betIndex = 1;
            bool first = true;
            
            List<ExcelExport> winList = new List<ExcelExport>();

            foreach (DateTime betDate in model.BetList.GroupBy(bl => bl.BetDate).Select(d => d.Key))
            {
                List<ExcelExport> betsOnDate = model.BetList.Where(g => g.BetDate == betDate).ToList();
                winList.Clear();
    
                foreach (ExcelExport game in betsOnDate)
                {
                    if (first)
                    {
                        game.RunningTotal = 2000;
                    }
                    else
                    {
                        game.RunningTotal = model.BetList[model.BetList.IndexOf(game) - 1].RunningTotal;
                    }
                    first = false;

                    switch (game.Current)
                    {
                        case "A":
                            game.BetAmount = aBet;
                            break;
                        case "B":
                            game.BetAmount = bBet;
                            break;
                        case "C":
                            game.BetAmount = cBet;
                            break;
                        case "D":
                            game.BetAmount = dBet;
                            break;
                        //case "Loss":
                        //    game.bet
                        //    break;
                        default:
                            //game.Current = "OVER";
                            game.BetAmount = 0;
                            break;
                    }

                    //if (game.RunningTotal < game.BetAmount)
                    //{
                    //    game.BankDump = game.RunningTotal - game.BetAmount;
                    //}

                    game.RunningTotal = game.RunningTotal - game.BetAmount;

                    if (game.Winning == game.Current)
                    {
                        ExcelExport ex = new ExcelExport();
                        ex.AwayTeam = game.AwayTeam;
                        ex.BankDump = game.BankDump;
                        ex.BetAmount = game.BetAmount;
                        ex.BetDate = game.BetDate;
                        ex.BetId = game.BetId;
                        ex.Current = game.Current;
                        ex.HomeTeam = game.HomeTeam;
                        ex.RunningTotal = game.RunningTotal;
                        ex.WinAmount = game.WinAmount;
                        ex.Winning = game.Winning;

                        
                        //game.RunningTotal = game.RunningTotal + game.BetAmount + game.WinAmount;
                        //ex.BetAmount = 0;

                        winList.Add(ex);
                        //betIndex++;
                        //if (betIndex == 18)
                        //{
                        //    game.BankDump = dumpAmount;
                        //    game.RunningTotal = game.RunningTotal - dumpAmount;
                        //}
                    }
                    //else
                    //{
                    //    game.RunningTotal = game.RunningTotal - game.BetAmount;
                    //}
                }

                int index = 0;
                winList = winList.OrderBy(wl => wl.RunningTotal).ToList();
                foreach (ExcelExport win in winList)
                //foreach (ExcelExport win in winList)
                {
                    index = winList.IndexOf(win);
                    if (index > 0)
                    {
                        win.RunningTotal = winList[index - 1].RunningTotal;
                    }

                    win.WinAmount = (win.BetAmount - (win.BetAmount * .1));
                    win.RunningTotal = win.RunningTotal + win.BetAmount + win.WinAmount;
                    //model.BetList.Insert(model.BetList.IndexOf(betsOnDate.Where(b=>b.BetDate == win.BetDate).Last()) +1, win);
                    index++;
                }
                winList = winList.OrderBy(wl => wl.RunningTotal).ToList();
                model.BetList.InsertRange(model.BetList.IndexOf(betsOnDate.Last()) + 1, winList);
                //if (winList.Count > 0)
                //{
                //    model.BetList.InsertRange(model.BetList.IndexOf(betsOnDate.Last()), winList);
                //}
            }
        }


        public static void MBLVersionOneBets(this SeasonBets sBets, AppStatic.Seasons season, AppStatic.BettingSetup bettingSet)
        {
            ListAccess la = new ListAccess();
            List<Game> seasonGames = la.MLBBetPotentials((int)season);
            List<Game> teamGames;
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();
            bool hasWon = false;
            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;
            int gameCount = 0;
            foreach (Team team in AppStatic.MLBTeams)
            {
            }
        }

    }
}