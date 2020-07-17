﻿using System;
using SQLite;

namespace VirtualPiggyBank.Core
{

    [Table("Transactions")]
    public class Transaction
    {

        [PrimaryKey]
        public Guid TransactionID { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double TransAmount { get; set; }

        public string Note { get; set; }

        public Transaction()
        {
        }
    }
}
