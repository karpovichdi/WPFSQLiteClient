namespace Turtur.Models
{
    class Supplier
    {
        private string name, category;

        private int id;

        public string Name { get; set; }

        public string Category { get; set; }

        public int Id { get; set; }

        public Supplier() { }

        public Supplier(string name, string category)
        {
            this.name = name;
            this.category = category;
        }
    }
}