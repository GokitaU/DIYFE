using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sports;

namespace DIYFE.Web.Models.Sports
{
    public class TeamSchedule
    {
        public int TeamId { get; set; }
        public List<Game> Games { get; set; } 
    }
}