using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class Money
    {
        private string transactionName;

        private int id, cost;

        public string TransactionName { get; set; }

        public int Cost { get; set; }

        public int Id { get; set; }

        public Money() { }

        public Money(string transactionName, int cost)
        {
            this.transactionName = transactionName;
            this.cost = cost;
        }
    }
}
