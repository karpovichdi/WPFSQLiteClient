namespace Turtur.Models
{
    public class Sale
    {
        public readonly string Date;
        public readonly int Customer;
        public readonly int Cat;
        public readonly int Id;

        public Sale(string date, int customer, int cat, int id)
        {
            Date = date;
            Customer = customer;
            Cat = cat;
            Id = id;
        }
    }
}