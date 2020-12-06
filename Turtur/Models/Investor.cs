namespace Turtur.Models
{
    public class Investor
    {
        public string Name;
        public readonly int Id;

        public string NAME
        {
            get => Name;
            set => Name = value;
        }

        public Investor(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}