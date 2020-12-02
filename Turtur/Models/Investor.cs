namespace Turtur.Models
{
    class Investor
    {
        private string name;

        private int id;

        public string Name { get; set; }

        public int Id { get; set; }

        public Investor() { }

        public Investor(string name)
        {
            this.name = name;
        }
    }
}