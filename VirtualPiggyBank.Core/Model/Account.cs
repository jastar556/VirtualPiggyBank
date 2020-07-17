using System;
using System.Collections.Generic;

namespace VirtualPiggyBank.Core
{
    public class Account
    {

        public string Name { get; set; }

        public Guid Id { get; set; }

        public double Balance { get; set; }

        public List<Transaction> Transactions { get; set; }

        public Account()
        {
        }

    }
}
