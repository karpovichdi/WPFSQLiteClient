namespace Turtur.Models
{
    public class Sale
    {
        public string Date;
        public int Customer;
        public int Cat;
        public readonly int Id;

        public string DATE
        {
            get => Date;
            set => Date = value;
        }

        public string Cost { get; set; }
        public string CatName { get; set; }
        public string CustomerName { get; set; }

        public Sale(int id, string date, int customer, int cat)
        {
            Date = date;
            Customer = customer;
            Cat = cat;
            Id = id;
        }
    }
}