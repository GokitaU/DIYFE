using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sports;

using DIYFE.Web.Models.Sports;

namespace DIYFE.Web.Controllers.Sports
{
    public class MLBController : ApplicationController
    {
        //
        // GET: /MLB/

        public ActionResult Index()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View("~/Views/Sports/MLB/Index.cshtml");
        }

        public ActionResult Scraping()
        {
            PageModel.ArticleContent.Title = "Bootstrap Home Page";
            PageModel.ArticleContent.Description = "Bootstrap Template Project";
            PageModel.ArticleContent.Author = "Bootstrap";
            PageModel.ArticleContent.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View("~/Views/Sports/MLB/Scraping.cshtml");
        }

        public ActionResult BetList(int seasonId, int? bettingType, int? moneyType)
        {
            SeasonBets model = new SeasonBets();
            model.MBLVersionOneBets((AppStatic.Seasons)seasonId, AppStatic.BettingSetup.MoneyLine);

            return View("~/Views/Sports/MLB/BetList.cshtml", model);
        }

        public ActionResult InsertMLBGameScrap(List<GameScraping> games)
        {
            List<Team> teams = AppStatic.MLBTeams;
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
                scrap.GameDate = scrap.GameDate.Substring(5, (scrap.GameDate.Length - 5));
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

    }
}
