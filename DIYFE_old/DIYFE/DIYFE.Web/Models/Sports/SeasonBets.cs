using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sports;

namespace DIYFE.Web.Models.Sports
{
    public class SeasonBets
    {
        private List<Bet> _bets = new List<Bet>();
        public List<Bet> Bets
        {
            get { return _bets; }
            set
            {
                _bets = value;
                //TotalA = _bets.Where(b => b.WinA).Count();
                //TotalB = _bets.Where(b => b.WinB).Count();
                //TotalC = _bets.Where(b => b.WinC).Count();
                //TotalD = _bets.Where(b => b.WinD).Count();
                //TotalWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread || g.WonMoneyLine).Count();
                ////TotalLoss = _bets.Where(b => !b.WinA && b.BetAGameId != 0).Count() 
                ////          + _bets.Where(b => !b.WinB && b.BetBGameId != 0).Count()
                ////          + _bets.Where(b => !b.WinC && b.BetCGameId != 0).Count()
                ////          + _bets.Where(b => !b.WinD && b.BetDGameId != 0).Count();
                //TotalLoss = _bets.SelectMany(b => b.BetGames).Where(g => !g.WonSpread && !g.WonMoneyLine).Count();
                //TotalGames = _bets.SelectMany(b => b.BetGames).Count();
                //TotalGroupLoss = _bets.Where(b => !b.WinA && !b.WinB && !b.WinC && !b.WinD).Count();
                //TotalGroupWin = _bets.Where(b => b.WinA || b.WinB || b.WinC || b.WinD).Count();
                //TotalSpreadWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread).Count();
                //TotalMoneyLineWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonMoneyLine).Count();
            }
        }

        //public List<Bet> TodaysBets { get; set; }
        public List<Bank> BankOne { get; set; }
        public List<Bank> BankTwo { get; set; }
        public List<Bank> BankThree { get; set; }

        public List<Game> TodaysBets { get; set; }

        public List<Game> TomorrowBets { get; set; }

        public void ReTotal()
        {
                TotalA = _bets.Where(b => b.WinA).Count();
                TotalB = _bets.Where(b => b.WinB).Count();
                TotalC = _bets.Where(b => b.WinC).Count();
                TotalD = _bets.Where(b => b.WinD).Count();
                TotalWin = TotalA + TotalB + TotalC + TotalD;
                TotalLoss = _bets.Where(b => !b.WinA && b.BetAGameId != 0).Count() 
                          + _bets.Where(b => !b.WinB && b.BetBGameId != 0).Count()
                          + _bets.Where(b => !b.WinC && b.BetCGameId != 0).Count()
                          + _bets.Where(b => !b.WinD && b.BetDGameId != 0).Count();
                TotalGames = _bets.SelectMany(b => b.BetGames).Count();
                TotalGroupLoss = _bets.Where(b => !b.WinA && !b.WinB && !b.WinC && !b.WinD).Count();
                TotalGroupWin = _bets.Where(b => b.WinA || b.WinB || b.WinC || b.WinD).Count();
                TotalSpreadWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread).Count();
        }

        //public void TotalGroupBetNumbers()
        //{
        //    TotalA = _bets.Where(b => b.WinA).Count();
        //    TotalB = _bets.Where(b => b.WinB).Count();
        //    TotalC = _bets.Where(b => b.WinC).Count();
        //    TotalD = _bets.Where(b => b.WinD).Count();
        //    //TotalWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread || g.WonMoneyLine).Count();
        //    //TotalLoss = _bets.Where(b => !b.WinA && b.BetAGameId != 0).Count()
        //    //          + _bets.Where(b => !b.WinB && b.BetBGameId != 0).Count()
        //    //          + _bets.Where(b => !b.WinC && b.BetCGameId != 0).Count()
        //    //          + _bets.Where(b => !b.WinD && b.BetDGameId != 0).Count();
        //    //TotalLoss = _bets.SelectMany(b => b.BetGames).Where(g => !g.WonSpread && !g.WonMoneyLine).Count();
        //    //TotalGames = _bets.SelectMany(b => b.BetGames).Count();
        //    TotalGroupLoss = _bets.Where(b => !b.WinA && !b.WinB && !b.WinC && !b.WinD).Count();
        //    TotalGroupWin = _bets.Where(b => b.WinA || b.WinB || b.WinC || b.WinD).Count();
        //    //TotalSpreadWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread).Count();
        //    //TotalMoneyLineWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonMoneyLine).Count();
        //}

        //public void TotalGameNumber()
        //{
        //    TotalGames = _bets.SelectMany(b => b.BetGames).Count();
        //    TotalWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread || g.WonMoneyLine).Count();
        //    TotalLoss = _bets.SelectMany(b => b.BetGames).Where(g => !g.WonSpread && !g.WonMoneyLine).Count();
        //    TotalSpreadWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonSpread).Count();
        //    TotalMoneyLineWin = _bets.SelectMany(b => b.BetGames).Where(g => g.WonMoneyLine).Count();
        //    //TotalA = _bets.Where(b => b.WinA).Count();
        //    //TotalB = _bets.Where(b => b.WinB).Count();
        //    //TotalC = _bets.Where(b => b.WinC).Count();
        //    //TotalD = _bets.Where(b => b.WinD).Count();
           
        //    //TotalLoss = _bets.Where(b => !b.WinA && b.BetAGameId != 0).Count() 
        //    //          + _bets.Where(b => !b.WinB && b.BetBGameId != 0).Count()
        //    //          + _bets.Where(b => !b.WinC && b.BetCGameId != 0).Count()
        //    //          + _bets.Where(b => !b.WinD && b.BetDGameId != 0).Count();
            
        //    //TotalGroupLoss = _bets.Where(b => !b.WinA && !b.WinB && !b.WinC && !b.WinD).Count();
        //    //TotalGroupWin = _bets.Where(b => b.WinA || b.WinB || b.WinC || b.WinD).Count();
            
        //}

        public int TotalA { get; set; }
        public int TotalB { get; set; }
        public int TotalC { get; set; }
        public int TotalD { get; set; }
        public int TotalWin { get; set; }
        public int TotalSpreadWin { get; set; }
        public int TotalMoneyLineWin { get; set; }
        public int TotalLoss { get; set; }
        public int TotalGames { get; set; }
        public int TotalGroupWin { get; set; }
        public int TotalGroupLoss { get; set; }

    }
}