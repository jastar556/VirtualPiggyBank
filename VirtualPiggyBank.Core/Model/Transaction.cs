using System;


namespace VirtualPiggyBank.Core
{
    public class Transaction
    {

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double TransAmount { get; set; }

        public string Note { get; set; }

        public Transaction()
        {
        }
    }
}
