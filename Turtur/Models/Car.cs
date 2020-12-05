namespace Turtur.Models
{
    public class Car
    {
        public readonly int Id;
        public string Name;

        public string NAME
        {
            get => Name;
            set => Name = value;
        }

        public Car(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}