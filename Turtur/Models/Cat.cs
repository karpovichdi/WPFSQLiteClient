namespace Turtur.Models
{
    public class Cat
    {
        public readonly string Name;
        public readonly int Weight;
        public readonly int Cost;
        public readonly int Id;

        public Cat(int id, int weight, int cost, string name)
        {
            Name = name;
            Weight = weight;
            Cost = cost;
            Id = id;
        }
    }
}