namespace Turtur.Models
{
    public class Customer
    {
        public string Name;
        public string Phone;
        public readonly int Id;

        public string NAME
        {
            get => Name;
            set => Name = value;
        }

        public string PHONE
        {
            get => Phone;
            set => Phone = value;
        }

        public Customer(int id, string name, string phone) 
        {
            Name = name;
            Phone = phone;
            Id = id;
        }
    }
}