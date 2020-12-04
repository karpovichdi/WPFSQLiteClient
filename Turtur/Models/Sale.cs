namespace Turtur.Models
{
    public class Sale
    {
        public readonly string Date;
        public readonly int Customer;
        public readonly int Cat;
        public readonly int Id;

        public Sale(int id, string date, int customer, int cat)
        {
            Date = date;
            Customer = customer;
            Cat = cat;
            Id = id;
        }
    }
}