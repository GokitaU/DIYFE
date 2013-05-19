using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sports;

using DIYFE.Web.Models.Sports;


namespace DIYFE.Web.Controllers
{
    public class NBAController : ApplicationController
    {
        //
        // GET: /Sports/

        public ActionResult Index()
        {

            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View("~/Views/Sports/NBA/Index.cshtml");
        }

        public ActionResult GameDetail(int gameId)
        {
            GameDetails model = new GameDetails();
            model.game = new Game(gameId);

            //ListAccess la = new ListAccess();
            SeasonBets sBets = new SeasonBets();
            sBets.VersionThree((AppStatic.Seasons)model.game.SeasonId, AppStatic.BettingSetup.MoneyLine);

            model.gameLinks = sBets.Bets.SelectMany(b => b.BetGames).Select(g => g.GameId).ToArray();

            return View("~/Views/Sports/NBA/GameDetail.cshtml", model);
        }

        public ActionResult TeamSchedule(int seasonId, int teamId)
        {
            TeamSchedule model = new DIYFE.Web.Models.Sports.TeamSchedule();
            model.TeamId = teamId;
            model.Games = new List<Game>();
            ListAccess la = new ListAccess();
            model.Games = la.TeamSchedule(seasonId, teamId);

            return View("~/Views/Sports/NBA/TeamSchedule.cshtml", model);
        }

        public ActionResult BetList(int seasonId, int? bettingType, int? moneyType)
        {
            SeasonBets model = new SeasonBets();
            //ListAccess la = new ListAccess();
            //model.Bets = la.NBABets(seasonId);
            //model.TotalGroupBetNumbers();

            model.VersionThree((AppStatic.Seasons)seasonId, AppStatic.BettingSetup.MoneyLine);
            //model.Bets = model.Bets.SelectMany(b => b.BetGames).OrderBy(g => g.GameDate).ToList();
            //model.MoneyLineBetsQuitWin();
            //if (bettingType != null)
            //{
            //    switch (bettingType)
            //    {
            //        case 1:
            //            model.Bets.SpreadBetsAll();
            //            break;
            //        case 2:
            //            model.Bets.MoneyLineBetsAll();
            //            break;
            //    }
            //}

            //model.Bets.MoneyLineBetsAll();



            return View("~/Views/Sports/NBA/BetList.cshtml", model);
        }

        public ActionResult Excel(int seasonId, int? bettingType, int? moneyType)
        {
            ExcelModel model = new ExcelModel();
            model.BetList = new List<ExcelExport>();
            SeasonBets tempModel = new SeasonBets();
            //ListAccess la = new ListAccess();
            //model.Bets = la.NBABets(seasonId);
            //model.TotalGroupBetNumbers();

            tempModel.VersionThree((AppStatic.Seasons)seasonId, AppStatic.BettingSetup.MoneyLine);
            //List<ExcelExport> eeList = new List<ExcelExport>();
            bool hasWon = false;

            //keeps track of amount of best after loss
            int indexLoss = 0;
            foreach (Bet bet in tempModel.Bets)
            {
                hasWon = false;
                foreach (Game game in bet.BetGames)
                {
                    if (hasWon)
                    { break; };

                    ExcelExport ee = new ExcelExport();
                    ee.BetId = bet.BetId;
                    ee.BetDate = game.GameDate;
                    ee.AwayTeam = game.AwayTeamName;
                    ee.HomeTeam = game.HomeTeamName;

                    switch (bet.BetGames.IndexOf(game))
                    {
                        case 0:
                            ee.Current = "A";
                            break;
                        case 1:
                            ee.Current = "B";
                            break;
                        case 2:
                            ee.Current = "C";
                            break;
                        case 3:
                            ee.Current = "D";
                            break;
                        case 4:
                            ee.Current = "Loss";
                            break;
                        default:
                            ee.Current = "OVER";
                            break;
                    }

                    ee.RunningTotal = ee.RunningTotal - ee.BetAmount;

                    if (bet.WinA)
                    {
                        ee.Winning = "A";
                    }
                    if (bet.WinB)
                    {
                        ee.Winning = "B";
                    }
                    if (bet.WinC)
                    {
                        ee.Winning = "C";
                    }
                    if (bet.WinD)
                    {
                        ee.Winning = "D";
                    }
                    if (!bet.WinA && !bet.WinB && !bet.WinC && !bet.WinD)
                    {
                        ee.Winning = "Loss";
                    }

                    model.BetList.Add(ee);

                    if (ee.Winning == ee.Current)
                    {
                        hasWon = true;
                    }
                }
            }

            model.BankRoll(16, 2000, 275, 578, 1214, 2275);

            model.ACount = model.BetList.Where(b => b.Winning == "A" && b.Current == "A").Count();
            model.BCount = model.BetList.Where(b => b.Winning == "B" && b.Current == "B").Count();
            model.CCount = model.BetList.Where(b => b.Winning == "C" && b.Current == "C").Count();
            model.DCount = model.BetList.Where(b => b.Winning == "D" && b.Current == "D").Count();
            model.Loss = model.BetList.Where(b => b.Winning == "Loss" && b.Current == "Loss").Count();
            model.TotalWins = model.ACount + model.BCount + model.CCount + model.DCount;
            model.TotalBets = model.BetList.Count();

            // model.VersionThree((AppStatic.Seasons)seasonId, AppStatic.BettingSetup.MoneyLine);
            //model.Bets = model.Bets.SelectMany(b => b.BetGames).OrderBy(g => g.GameDate).ToList();
            //model.MoneyLineBetsQuitWin();
            //if (bettingType != null)
            //{
            //    switch (bettingType)
            //    {
            //        case 1:
            //            model.Bets.SpreadBetsAll();
            //            break;
            //        case 2:
            //            model.Bets.MoneyLineBetsAll();
            //            break;
            //    }
            //}

            //model.Bets.MoneyLineBetsAll();


            return View("~/Views/Sports/NBA/Excel.cshtml", model);
        }

        public ActionResult ByDate(int seasonId, int? bettingType, int? moneyType)
        {
            List<ByDate> model = new List<ByDate>();

            SeasonBets tempModel = new SeasonBets();
            List<DateTime> betDates = new List<DateTime>();

            tempModel.VersionThree((AppStatic.Seasons)seasonId, AppStatic.BettingSetup.MoneyLine);
            betDates = tempModel.Bets.SelectMany(b => b.BetGames).GroupBy(g => g.GameDate).OrderBy(g => g.Key).Select(g => g.Key).ToList();

            foreach (DateTime gameDate in betDates)
            {
                ByDate day = new ByDate();
                day.Date = gameDate;
                day.ACount = tempModel.Bets.Where(b => b.WinA && b.BetADate <= gameDate).Count();
                day.BCount = tempModel.Bets.Where(b => b.WinB && b.BetBDate <= gameDate).Count();
                day.CCount = tempModel.Bets.Where(b => b.WinC && b.BetCDate <= gameDate).Count();
                day.DCount = tempModel.Bets.Where(b => b.WinD && b.BetDDate <= gameDate).Count();
                day.Loss = tempModel.Bets.Where(b => b.Loss && b.LossDate <= gameDate).Count();
                //day.TotalBets = tempModel.Bets.Where(b => b.Loss && b.BetDDate <= gameDate).Count();
                model.Add(day);
            }

            return View("~/Views/Sports/NBA/ByDate.cshtml", model);
        }



        #region NBA

        public ActionResult BetDetails()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            //List<Team> team = AppStatic.NBATeams;

            return View("~/Views/Sports/NBA/BetDetails.cshtml");
        }

        public ActionResult BetGameList(int seasonId, int? bettingType, int? moneyType)
        {
            SeasonBets model = new SeasonBets();
            ListAccess la = new ListAccess();
            model.Bets = la.NBABets(seasonId);
            //model.MoneyLineBetsQuitWin();
            //if (bettingType != null)
            //{
            //    switch (bettingType)
            //    {
            //        case 1:
            //            model.Bets.SpreadBetsAll();
            //            break;
            //        case 2:
            //            model.Bets.MoneyLineBetsAll();
            //            break;
            //    }
            //}

            model.Bets.MoneyLineBetsAll();



            return View("~/Views/Sports/NBA/BetGameList.cshtml", model);
        }

        public ActionResult Scraping()
        {

            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View("~/Views/Sports/NBA/Scraping.cshtml");
        }

        public ActionResult BankTest(int bettingType)
        {
            List<Bank> season = new List<Bank>();
            
            Bank bank = new Bank();
            bank.BankStart = 800;
            bank.CurrentBank = 800;
            bank.WinningCurrent = 100;
            bank.WinningTotal = 0;

            season.Add(bank);

            for (int i = 0; i < 100; i++)
            {
                Bank nextBank = new Bank(season[i]);
                nextBank.Win();
               // nextBank.CashOut = 0;
                season.Add(nextBank);

                switch (bettingType)
                {
                    case 0:
                        break;
                    case 1:
                        //
                        #region
                        
                        if (i == 10 || i == 20 || i == 30 || i == 40 || i == 50 || i == 60)
                        {
                            int newWinningTotal = (nextBank.WinningTotal / 2);
                            //int newWinningTotal = (nextBank.WinningTotal / 2);
                            int nextCurrentBank = nextBank.CurrentBank - newWinningTotal;
                            Bank newBank = new Bank();
                            Bank tempBank = season.Where(s => s.RequiredBank <= nextCurrentBank).LastOrDefault();
                            newBank.ABetAmount = tempBank.ABetAmount;
                            newBank.BankStart = tempBank.BankStart;
                            newBank.BBetAmount = tempBank.BBetAmount;
                            newBank.CashOut = tempBank.CashOut;
                            newBank.CBetAmount = tempBank.CBetAmount;
                            newBank.CurrentBank = tempBank.CurrentBank;
                            newBank.DBetAmount = tempBank.DBetAmount;
                            newBank.RequiredBank = tempBank.RequiredBank;
                            newBank.WinningCurrent = tempBank.WinningCurrent;
                            newBank.WinningTotal = tempBank.WinningTotal;

                            newWinningTotal = newWinningTotal + (nextCurrentBank - tempBank.RequiredBank);
                            newBank.CurrentBank = newBank.RequiredBank;
                            newBank.WinningTotal = 0;
                            newBank.CashOut = newWinningTotal;

                            newBank.Win();
                            season.Add(newBank);
                            i++;

                        }

                        #endregion
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                }



                
            }
            return View("~/Views/Sports/NBA/BankTest.cshtml", season);
        }

        #endregion

        #region Services

        public ActionResult InsertNBAGameScrap(List<GameScraping> games)
        {
            List<Team> teams = AppStatic.NBATeams;
            List<Game> gameList = new List<Game>();

            foreach (GameScraping scrap in games)
            {

                if (scrap.GameScore.Contains("F"))
                {
                    scrap.GameScore = scrap.GameScore.Remove(scrap.GameScore.IndexOf('F'), scrap.GameScore.Length - scrap.GameScore.IndexOf('F'));
                }
                if (scrap.GameScore.Contains("OT"))
                {
                    scrap.GameScore = scrap.GameScore.Replace("OT", "");
                }


                string[] score = scrap.GameScore.Split('-');

                Game game = new Game();
                DateTime dateVal;

                game.SeasonId = scrap.SeasonId;
               scrap.GameDate =  scrap.GameDate.Substring(5, (scrap.GameDate.Length-5));
                if (DateTime.TryParse(scrap.GameDate, out dateVal))
                { game.GameDate = dateVal; }
                game.WinningTeamId = 0;
                game.HomeTeamScore = 0;
                game.AwayTeamScore = 0;
                game.GameStatusTypeId = 2;

                if (scrap.Location == "@")
                {
                    game.HomeTeamId = teams.Where(t => t.ScrapId == scrap.TeamTwoScrapId.Trim()).Select(t => t.TeamId).FirstOrDefault();
                    game.AwayTeamId = scrap.TeamOneId;
                }
                else
                {
                    game.HomeTeamId = scrap.TeamOneId;
                    game.AwayTeamId = teams.Where(t => t.ScrapId == scrap.TeamTwoScrapId.Trim()).Select(t => t.TeamId).FirstOrDefault();
                }

                if (scrap.GameStatus != "Scheduled" && scrap.GameStatus != "Postponed")
                {
                    game.GameStatusTypeId = 1;
                    score[0] = score[0].Replace(" 1st", "").Replace(" 2nd", "").Replace(" 3rd", "").Replace(" 4th", "").Replace(" 5th", "").Replace(" 6th", "").Replace(" 7th", "").Replace(" 8th", "").Replace(" 9th", "");
                    score[1] = score[1].Replace(" 1st", "").Replace(" 2nd", "").Replace(" 3rd", "").Replace(" 4th", "").Replace(" 5th", "").Replace(" 6th", "").Replace(" 7th", "").Replace(" 8th", "").Replace(" 9th", "");


                    if (game.HomeTeamId == scrap.TeamOneId && scrap.GameStatus == "W")
                    {
                        game.HomeTeamScore = Convert.ToInt32(score[0]);
                        game.AwayTeamScore = Convert.ToInt32(score[1]);
                    }
                    else if (game.HomeTeamId == scrap.TeamOneId && scrap.GameStatus == "L")
                    {
                        game.HomeTeamScore = Convert.ToInt32(score[1]);
                        game.AwayTeamScore = Convert.ToInt32(score[0]);
                    }
                    if (game.AwayTeamId == scrap.TeamOneId && scrap.GameStatus == "W")
                    {
                        game.AwayTeamScore = Convert.ToInt32(score[0]);
                        game.HomeTeamScore = Convert.ToInt32(score[1]);
                    }
                    else if (game.AwayTeamId == scrap.TeamOneId && scrap.GameStatus == "L")
                    {
                        game.AwayTeamScore = Convert.ToInt32(score[1]);
                        game.HomeTeamScore = Convert.ToInt32(score[0]);
                    }

                    if (game.HomeTeamScore > game.AwayTeamScore)
                    {
                        game.WinningTeamId = game.HomeTeamId;
                    }
                    else
                    {
                        game.WinningTeamId = game.AwayTeamId;
                    }
                }
                else
                {
                    if (scrap.GameStatus == "Postponed")
                    {
                        game.GameStatusTypeId = 4;
                    }
                }
                //game.TeamOneId = scrap.TeamOneId;
                //game.TeamTwoId = teams.Where(t => t.ScrapId == scrap.TeamTwoScrapId.Trim()).Select(t => t.TeamId).FirstOrDefault();

                //switch (scrap.GameStatus)
                //{
                //    case "W":
                //        game.GameStatusTypeId = 1;
                //        game.WinningTeamId = game.TeamOneId;
                //        game.TeamOneScore = Convert.ToInt32(score[0]);
                //        game.TeamTwoScore = Convert.ToInt32(score[1]);
                //        break;
                //    case "L":
                //        game.GameStatusId = 2;
                //        game.WinningTeamId = game.TeamTwoId;
                //        game.TeamOneScore = Convert.ToInt32(score[1]);
                //        game.TeamTwoScore = Convert.ToInt32(score[0]);
                //        break;
                //    case "Scheduled":
                //        game.GameStatusId = 3;
                //        game.TeamOneScore = 0;
                //        game.TeamTwoScore = 0;
                //        break;

                //}


                game.InsertGame();

                gameList.Add(game);
            }

            var test = games;

            var data = new Object { };

            // DataAccess da = new DataAccess();
            //List<Team> teams = da.AllTeam();

            data = new { success = true, teams = teams };

            return Json(data);
        }

        public ActionResult InsertNBABetting(int seasonId)
        {
            var data = new Object { };

            ListAccess la = new ListAccess();
            List<Game> NBAGames = la.AllSeasonGames(seasonId, 2);

            List<Team> teams = AppStatic.NBATeams;
            List<Game> gameList = new List<Game>();
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();

            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;

            int gameCount = 0;

            foreach (Team team in teams)
            {
                gameList = NBAGames.Where(g => g.AwayTeamId == team.TeamId && g.AwayTeamConferenceId != g.HomeTeamConferenceId).OrderBy(g => g.GameDate).ToList();

                for (int i = 0; i < gameList.Count(); i++)
                {
                    if (i != 0)
                    {
                        previousDate = gameList[i-1].GameDate;
                        ts = gameList[i].GameDate - previousDate;
                        if (ts.Days <= 3)
                        {
                            gameCount++;
                            if (gameCount >= 2)
                            {
                                if (i - 2 >= 0)
                                {
                                    bettingSeries.Add(gameList[i - 2]);
                                }
                                bettingSeries.Add(gameList[i-1]);
                                bettingSeries.Add(gameList[i]);

                                Bet bet = new Bet();
                                bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
                                bet.SeasonId = bettingSeries[0].SeasonId;
                                bet.BetStatus = new List<BetStatus>();
                                foreach (Game game in bettingSeries)
                                {
                                    switch (bettingSeries.IndexOf(game))
                                    {
                                        case 0:
                                            bet.BetADate = game.GameDate;
                                            bet.BetAGameId = game.GameId;
                                            if (game.WinningTeamId == game.AwayTeamId)
                                            {
                                                bet.WinningBet = "A";
                                                bet.BetStatus.Add(new BetStatus { BetStatusTypeId=1 });
                                                bet.LosingBetTeamId = game.HomeTeamId;
                                            }
                                            break;
                                        case 1:
                                            bet.BetBDate = game.GameDate;
                                            bet.BetBGameId = game.GameId;
                                            if (game.WinningTeamId == game.AwayTeamId)
                                            {
                                                bet.WinningBet = "B";
                                                bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
                                                bet.LosingBetTeamId = game.HomeTeamId;
                                            }
                                            break;
                                        case 2: 
                                            bet.BetCDate = game.GameDate;
                                            bet.BetCGameId = game.GameId;
                                            if (game.WinningTeamId == game.AwayTeamId)
                                            {
                                                bet.WinningBet = "C";
                                                bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
                                                bet.LosingBetTeamId = game.HomeTeamId;
                                            }
                                            break;
                                    }
                                }
                                if (bet.BetStatus.Count() == 0)
                                {
                                    if (bet.BetCDate.HasPlayed())
                                    {
                                        bet.WinningBet = "Loss";
                                        bet.LosingBetTeamId = bet.WinningBetTeamId;
                                        bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
                                    }
                                }

                                bet.InsertBet();

                                gameCount = 0;
                                bettingSeries.Clear();
                                //bet.BetADate = bettingSeries[0].GameDate;
                                //bet.BetBDate = bettingSeries[1].GameDate;
                                //if (bettingSeries.Count() > 2)
                                //{
                                //    bet.BetCDate = bettingSeries[2].GameDate;
                                //}

                            }
                        }
                        else
                        {
                            gameCount = 0;
                            bettingSeries.Clear();
                        }
                    }
                }

                gameList.Clear();
            }


            data = new { success = true };

            return Json(data);
        }

        public ActionResult CreateNBABetting(int seasonId)
        {
            var data = new Object { };

            ListAccess la = new ListAccess();
            List<Game> NBAGames = la.AllSeasonGames(seasonId, 2);

            List<Team> teams = AppStatic.NBATeams;
            List<Game> gameList = new List<Game>();
            List<Game> bettingSeries = new List<Game>();
            List<Bet> bettingOpps = new List<Bet>();

            TimeSpan ts;
            DateTime previousDate = DateTime.MinValue;

            int gameCount = 0;

            foreach (Team team in teams)
            {
                gameList = NBAGames.Where(g => g.AwayTeamId == team.TeamId && g.AwayTeamConferenceId != g.HomeTeamConferenceId).OrderBy(g => g.GameDate).ToList();
                for (int i = 0; i < gameList.Count(); i++)
                {
                    if (i != 0)
                    {
                        previousDate = gameList[i - 1].GameDate;
                        ts = gameList[i].GameDate - previousDate;
                        gameCount = i;
                        try
                        {
                            while (ts.Days <= 3)
                            {
                                bettingSeries.Add(gameList[gameCount - 1]);
                                ts = gameList[gameCount].GameDate - bettingSeries.Last().GameDate;
                                gameCount++;
                                //if (gameCount > gameList.Count)
                                //{ break;  }
                            }
                        }
                        catch (Exception ex)
                        {
                            //probably index out of range error
                        }
                        if (gameCount > i)
                        { i = gameCount - 1; }

                        #region 
                        if (bettingSeries.Count >= 2)
                        {
                            Bet bet = new Bet();
                            bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
                            bet.SeasonId = bettingSeries[0].SeasonId;
                            bet.BetStatus = new List<BetStatus>();
                            foreach (Game game in bettingSeries)
                            {
                                switch (bettingSeries.IndexOf(game))
                                {
                                    case 0:
                                        bet.BetADate = game.GameDate;
                                        bet.BetAGameId = game.GameId;
                                        if (game.WinningTeamId == game.AwayTeamId)
                                        {
                                            //bet.WinningBet = "A";
                                            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 1 });
                                            // bet.LosingBetTeamId = game.HomeTeamId;
                                        }
                                        break;
                                    case 1:
                                        bet.BetBDate = game.GameDate;
                                        bet.BetBGameId = game.GameId;
                                        if (game.WinningTeamId == game.AwayTeamId)
                                        {
                                            //bet.WinningBet = "B";
                                            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
                                            //bet.LosingBetTeamId = game.HomeTeamId;
                                        }
                                        break;
                                    case 2:
                                        bet.BetCDate = game.GameDate;
                                        bet.BetCGameId = game.GameId;
                                        if (game.WinningTeamId == game.AwayTeamId)
                                        {
                                            //bet.WinningBet = "C";
                                            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
                                            // bet.LosingBetTeamId = game.HomeTeamId;
                                        }
                                        break;
                                    case 3:
                                        bet.BetDDate = game.GameDate;
                                        bet.BetDGameId = game.GameId;
                                        if (game.WinningTeamId == game.AwayTeamId)
                                        {
                                            //bet.WinningBet = "C";
                                            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 7 });
                                            // bet.LosingBetTeamId = game.HomeTeamId;
                                        }
                                        break;
                                }
                            }
                            if (bet.BetStatus.Count() == 0)
                            {
                                if (bet.BetCDate.HasPlayed())
                                {
                                    bet.WinningBet = "Loss";
                                    bet.LosingBetTeamId = bet.WinningBetTeamId;
                                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
                                }
                            }

                            bet.InsertBet();

                           // gameCount = 0;
                        }

                        #endregion
                        
                        bettingSeries.Clear();
                    }
                }
                gameList.Clear();
            }


            //foreach (Team team in teams)
            //{
            //    gameList = NBAGames.Where(g => g.AwayTeamId == team.TeamId && g.AwayTeamConferenceId != g.HomeTeamConferenceId).OrderBy(g => g.GameDate).ToList();

            //    for (int i = 0; i < gameList.Count(); i++)
            //    {
            //        if (i != 0)
            //        {
            //            previousDate = gameList[i - 1].GameDate;
            //            ts = gameList[i].GameDate - previousDate;
            //            if (ts.Days <= 3)
            //            {
            //                gameCount++;
            //                if (gameCount >= 2)
            //                {
            //                    if (i - 2 >= 0)
            //                    {
            //                        bettingSeries.Add(gameList[i - 2]);
            //                    }
            //                    bettingSeries.Add(gameList[i - 1]);
            //                    bettingSeries.Add(gameList[i]);

            //                    Bet bet = new Bet();
            //                    bet.WinningBetTeamId = bettingSeries[0].AwayTeamId;
            //                    bet.SeasonId = bettingSeries[0].SeasonId;
            //                    bet.BetStatus = new List<BetStatus>();
            //                    foreach (Game game in bettingSeries)
            //                    {
            //                        switch (bettingSeries.IndexOf(game))
            //                        {
            //                            case 0:
            //                                bet.BetADate = game.GameDate;
            //                                bet.BetAGameId = game.GameId;
            //                                if (game.WinningTeamId == game.AwayTeamId)
            //                                {
            //                                    //bet.WinningBet = "A";
            //                                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 1 });
            //                                   // bet.LosingBetTeamId = game.HomeTeamId;
            //                                }
            //                                break;
            //                            case 1:
            //                                bet.BetBDate = game.GameDate;
            //                                bet.BetBGameId = game.GameId;
            //                                if (game.WinningTeamId == game.AwayTeamId)
            //                                {
            //                                    //bet.WinningBet = "B";
            //                                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 3 });
            //                                    //bet.LosingBetTeamId = game.HomeTeamId;
            //                                }
            //                                break;
            //                            case 2:
            //                                bet.BetCDate = game.GameDate;
            //                                bet.BetCGameId = game.GameId;
            //                                if (game.WinningTeamId == game.AwayTeamId)
            //                                {
            //                                    //bet.WinningBet = "C";
            //                                    bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 5 });
            //                                   // bet.LosingBetTeamId = game.HomeTeamId;
            //                                }
            //                                break;
            //                        }
            //                    }
            //                    if (bet.BetStatus.Count() == 0)
            //                    {
            //                        if (bet.BetCDate.HasPlayed())
            //                        {
            //                            bet.WinningBet = "Loss";
            //                            bet.LosingBetTeamId = bet.WinningBetTeamId;
            //                            bet.BetStatus.Add(new BetStatus { BetStatusTypeId = 26 });
            //                        }
            //                    }

            //                    bet.InsertBet();

            //                    gameCount = 0;
            //                    bettingSeries.Clear();
            //                    //bet.BetADate = bettingSeries[0].GameDate;
            //                    //bet.BetBDate = bettingSeries[1].GameDate;
            //                    //if (bettingSeries.Count() > 2)
            //                    //{
            //                    //    bet.BetCDate = bettingSeries[2].GameDate;
            //                    //}

            //                }
            //            }
            //            else
            //            {
            //                gameCount = 0;
            //                bettingSeries.Clear();
            //            }
            //        }
            //    }

            //    gameList.Clear();
            //}


            data = new { success = true };

            return Json(data);
        }

        public ActionResult UpdateGameLines(Game game)
       // public ActionResult UpdateGameLines(int gameId, decimal homeLine, decimal awayLine)
        {
            var data = new object();
            //Game game = new Game();
            game.UpdateGameLine();

            data = new { success = true };
            return Json(data);
        }

        public ActionResult GetGameDates(int seasonId)
        {
            var data = new Object { };
            List<string> gameDatesFormated = new List<string>();
            ListAccess la = new ListAccess();
            List<DateTime> games = la.AllSeasonGames(seasonId, (int)AppStatic.Leagues.NBA).Select(g => g.GameDate).Distinct().ToList();
            foreach (DateTime date in games)
            {
                gameDatesFormated.Add(date.ToString("yyyyMMdd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
            }

            data = new { success = true, dates=gameDatesFormated };

            return Json(data);
        }

        public ActionResult GetGamesOnDate(string date)
        {
            var data = new Object { };

            data = new { success = true };

            return Json(data);
        }

        #endregion

    }
}
