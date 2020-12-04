namespace Turtur.Models
{
    public class Investor
    {
        public readonly string Name;
        public readonly int Id;

        public Investor(int id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}