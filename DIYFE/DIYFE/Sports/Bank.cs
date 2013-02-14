using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sports
{
    public class Bank
    {
        //RUNNING TOTAL OF BANK WINNINGS
        public int WinningTotal {get;set;}
        //CURRENT AMOUNT TRYING TO WIN
        public int WinningCurrent {get;set;}
        //CURRENT AMOUNT OF MONEY IN BANK
        public int CurrentBank {get;set;}
        //REQUIRED BANK TO MAKE WinningCurrent POSSABLE
        public int RequiredBank {get;set;}
        //AMOUNT OF MONEY TO START BANK WITH AND THE AMOUNT OF MONEY TO RESTART BANK IF LOSS BET
        public int BankStart {get;set;}
        //EACH AMOUNT IN THE CHASE BET TO MAKE WinningCurrent POSSABLE
        public int ABetAmount {get;set;}
        public int BBetAmount {get;set;}
        public int CBetAmount {get;set;}
        public int DBetAmount {get;set;}

        public int CashOut {get;set;}

        public Bank() { }

        public Bank(Bank bank){
            
            this.WinningTotal = bank.WinningTotal;
            //if (this.CurrentBank >= this.BankStart)
            //{
            //}
            this.WinningCurrent = (int)((bank.WinningCurrent * .11) + bank.WinningCurrent);
            this.CurrentBank = bank.CurrentBank;
            this.BankStart = bank.BankStart;

            //COULD CHANGE THIS TO USE GET AND SET, PROBABLY LOOK ALOT NICER AND EASIER TO UNDERSTAND
            int temp =0;
            this.ABetAmount = (int)((this.WinningCurrent *.1) + this.WinningCurrent);
            
            temp = this.ABetAmount + this.WinningCurrent;
            this.BBetAmount = (int)((temp * .1) + temp);

            temp = this.BBetAmount + this.ABetAmount + this.WinningCurrent;            
            this.CBetAmount = (int)((temp * .1) + temp);

            temp = this.CBetAmount + this.BBetAmount + this.ABetAmount + this.WinningCurrent;
            this.DBetAmount = (int)((temp * .1) + temp);

            this.RequiredBank = this.ABetAmount + this.BBetAmount + this.CBetAmount;// +this.DBetAmount;

        }
        
        public void Win() {
            this.WinningTotal = this.WinningTotal + this.WinningCurrent;
            this.CurrentBank = this.CurrentBank + this.WinningCurrent;
        }

        public void Loss() {
            
        }

        public void Rebuild()
        {
        }

    }
}
