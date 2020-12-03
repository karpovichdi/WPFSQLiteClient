namespace Turtur.Models
{
    public class Car
    {
        public readonly string Name;
        public readonly int Id;

        public Car(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}