namespace Turtur.Models
{
    public class OrgEvent
    {
        public readonly int Id;
        public readonly int Name;
        public readonly int Investors;
        public readonly int Cars;
        public readonly int Sales;
        public readonly int Customer;
        public readonly int Resources;
        public readonly int Money;
        public readonly int Cats;
        public readonly int Employees;
        
        public OrgEvent(int name, int investors, int cars, int sales, int customer, int resources, int money, int cats, int employees, int id)
        {
            Name = name;
            Investors = investors;
            Cars = cars;
            Sales = sales;
            Customer = customer;
            Resources = resources;
            Money = money;
            Cats = cats;
            Employees = employees;
            Id = id;
        }
    }
}