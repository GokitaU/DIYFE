using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sports;

using DIYFE.Web.Models.Sports;

namespace DIYFE.Web.Controllers.Sports
{
    public class SportsController : ApplicationController
    {

        public ActionResult Index()
        {
            PageModel.Title = "Bootstrap Home Page";
            PageModel.Description = "Bootstrap Template Project";
            PageModel.Author = "Bootstrap";
            PageModel.Keywords = "Boostrap project, starter project, soe keywords, keywords";
            PageModel.ActiveTopNavLink = "MainNavHome";

            return View();
        }

        /// <summary>
        /// This are shaired services for all sports
        /// </summary>
        #region Services

        public ActionResult GetAllTeams(int leagueId)
        {
            var data = new Object { };
            List<Team> teams = new List<Team>();
            try
            {
                switch (leagueId)
                {
                    case 1:
                        teams = AppStatic.MLBTeams;
                        break;
                    case 2:
                        teams = AppStatic.NBATeams;
                        break;
                    case 3:
                        teams = AppStatic.NHLTeams;
                        break;
                    case 4:
                        teams = AppStatic.NFLTeams;
                        break;
                    default:
                        throw new Exception("Incorrect leagueId");
                    //break;
                }

            }
            catch (Exception ex)
            {
                data = new { success = false, message = ex.Message };
                return Json(data);
            }

            data = new { success = true, teams = teams };

            return Json(data);
        }

        public ActionResult GetTeam(int teamId)
        {
            var data = new Object { };
            Team team = new Team(teamId);

            data = new { success = true, team = team };

            return Json(data);
        }

        public ActionResult GetGameDetails(int gameId)
        {
            var data = new Object { };
            Game game = new Game(gameId);
            if (game.HomeTeamId == 0)
            {
                data = new { success = false, error= "faild to load game"};
            }
            else
            {
                data = new { success = true, game = game };
            }

            return Json(data);
        }

        #endregion
    }
}
