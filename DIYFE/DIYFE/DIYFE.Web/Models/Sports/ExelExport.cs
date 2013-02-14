using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFE.Web.Models.Sports
{
    public class ExcelExport
    {
        public int BetId { get; set; }
        public DateTime BetDate { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
        public string Current { get; set; }
        public string Winning { get; set; }

        public int BetAmount { get; set; }
        public double WinAmount { get; set; }
        public double RunningTotal { get; set; }
        public double BankDump { get; set; }

    }
}