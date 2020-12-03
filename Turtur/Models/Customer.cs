namespace Turtur.Models
{
    public class Customer
    {
        public readonly string Name;
        public readonly string Phone;
        public readonly int Id;

        public Customer(int id, string name, string phone) 
        {
            Name = name;
            Phone = phone;
            Id = id;
        }
    }
}