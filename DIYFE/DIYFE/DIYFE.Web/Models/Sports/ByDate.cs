using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFE.Web.Models.Sports
{
    public class ByDate
    {
        public DateTime Date { get; set; }
        
        public int ACount { get; set; }
        public int BCount { get; set; }
        public int CCount { get; set; }
        public int DCount { get; set; }
        public int Loss { get; set; }

        public int TotalBets { get; set; }
        public int TotalWins { get; set; }
    }
}