namespace Turtur.Models
{
    public class Resource
    {
        public readonly string Name;
        public readonly int Supplier;
        public readonly int Id;
        
        public Resource(int id, string name, int supplier)
        {
            Supplier = supplier;
            Name = name;
            Id = id;
        }
    }
}