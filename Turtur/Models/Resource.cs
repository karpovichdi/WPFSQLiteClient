namespace Turtur.Models
{
    public class Resource
    {
        public readonly string Name;
        public readonly int Supplier;
        public readonly int Id;
        
        public Resource(string name, int supplier, int id)
        {
            Supplier = supplier;
            Name = name;
            Id = id;
        }
    }
}