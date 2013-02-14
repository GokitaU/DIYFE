using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFE.Web.Models.Sports
{
    public class GameScraping
    {
        public int SeasonId { get; set; }
        public int TeamOneId { get; set; }
        public string TeamTwoScrapId { get; set; }
        public string GameDate { get; set; }
        public string GameTime { get; set; }
        public string Location { get; set; }
        public string GameStatus { get; set; }
        public string GameScore { get; set; }
    }
}