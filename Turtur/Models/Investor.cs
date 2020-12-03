namespace Turtur.Models
{
    public class Investor
    {
        public readonly string Name;
        public readonly int Id;

        public Investor(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}