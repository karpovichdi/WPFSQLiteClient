namespace Turtur.Models
{
    public class Supplier
    {
        public readonly string Name;
        public readonly string Category;
        public readonly int Id;
        
        public Supplier(string name, string category, int id)
        {
            Name = name;
            Category = category;
            Id = id;
        }
    }
}