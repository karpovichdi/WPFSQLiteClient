namespace Turtur.Models
{
    public class Supplier
    {
        public readonly string Name;
        public readonly string Category;
        public readonly int Id;
        
        public Supplier(int id, string name, string category)
        {
            Name = name;
            Category = category;
            Id = id;
        }
    }
}