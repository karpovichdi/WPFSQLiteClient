using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class Sale
    {
        private string date;

        private int id, customer, cat;

        public string Date { get; set; }

        public int Customer { get; set; }

        public int Cat { get; set; }

        public int Id { get; set; }

        public Sale() { }

        public Sale(string date, int customer, int cat)
        {
            this.date = date;
            this.customer = customer;
            this.cat = cat;
        }
    }
}
