using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class Cat
    {
        private string name;

        private int id, weight, cost;

        public string Name { get; set; }

        public int Id { get; set; }

        public int Weight { get; set; }

        public int Cost { get; set; }

        public Cat() { }

        public Cat(string name, int weight, int cost)
        {
            this.name = name;
            this.weight = weight;
            this.cost = cost;
        }
    }
}