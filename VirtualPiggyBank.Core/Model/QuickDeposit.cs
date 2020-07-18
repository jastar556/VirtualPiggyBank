using System;
using SQLite;

namespace VirtualPiggyBank.Core.Model
{
    [Table("QuickDepositTypes")]
    public class QuickDeposit
    {
        [PrimaryKey, Unique]
        public string QuickDepositType { get; set; }

        public double Amount { get; set; }

        public QuickDeposit()
        {
            
        }
    }
}
