using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class OrgEvent
    {
        private int id, name, investors, cars, sales, customer, resources, money, cats, employees;

        public int Id { get; set; }
        public int Name { get; set; }
        public int Investors { get; set; }
        public int Cars { get; set; }
        public int Sales { get; set; }
        public int Customer { get; set; }
        public int Resources { get; set; }
        public int Money { get; set; }
        public int Cats { get; set; }
        public int Employees { get; set; }


        public OrgEvent() { }

        public OrgEvent(int name, int investors, int cars, int sales, int customer, int resources, int money, int cats, int employees)
        {
            this.name = name;
            this.investors = investors;
            this.cars = cars;
            this.sales = sales;
            this.customer = customer;
            this.resources = resources;
            this.money = money;
            this.cats = cats;
            this.employees = employees;
        }
    }
}
