namespace Turtur.Models
{
    class Customer
    {
        private string name, phone;

        private int id;

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Id { get; set; }

        public Customer() { }

        public Customer(string name, string phone) 
        {
            this.name = name;
            this.phone = phone;
        }
    }
}