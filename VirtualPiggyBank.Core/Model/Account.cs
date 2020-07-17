using System;
using System.Collections.Generic;
using SQLite;

namespace VirtualPiggyBank.Core
{
    [Table("UserAccounts")]
    public class Account
    {
        [PrimaryKey, Unique]
        public Guid Id { get; set; }

        [MaxLength(50), Unique]
        public string Name { get; set; }

        public double Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Account()
        {
        }

    }
}
