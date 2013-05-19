using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sports;

namespace DIYFE.Web.Models.Sports
{
    public class GameDetails
    {
        public Game game { get; set; }
        public int[] gameLinks { get; set; }
    }
}