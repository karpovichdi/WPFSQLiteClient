namespace Turtur.Models
{
    class Car
    {
        private string name;

        private int id;

        public string Name { get; set; }

        public int Id { get; set; }

        public Car() { }

        public Car(string name)
        {
            this.name = name;
        }
    }
}