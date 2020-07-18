using System;
using System.Collections.Generic;
using VirtualPiggyBank.Core.Repo;

namespace VirtualPiggyBank.Core.Service
{
    public static class AccountCalculations
    {

         
        public static double CalculateBalance(Account account)
        {

            var db = BankRepository.Connection();

            string selectStatement = "SELECT * FROM Transactions WHERE Account = " + "'"+ account.Id.ToString() + "'";
            List<Transaction> Transactions = db.CreateCommand(selectStatement).ExecuteQuery<Transaction>();
            double runningTotal = 0 ;

            foreach(Transaction transaction in Transactions)
            { 
               runningTotal += transaction.TransAmount;
            }

            return runningTotal;
        }
    }
}
