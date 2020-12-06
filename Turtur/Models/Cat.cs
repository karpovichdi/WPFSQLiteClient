namespace Turtur.Models
{
    public class Cat
    {
        public string Name;
        public string Photo;
        public int Weight;
        public int Cost;
        public readonly int Id;

        public string NAME
        {
            get => Name;
            set => Name = value;
        }
        
        public string PHOTO
        {
            get => Photo;
            set => Photo = value;
        }
        
        public int WEIGHT
        {
            get => Weight;
            set => Weight = value;
        }
        
        public int COST
        {
            get => Cost;
            set => Cost = value;
        }
        
        public Cat(int id, string photo, int weight, int cost, string name)
        {
            Name = name;
            Photo = photo;
            Weight = weight;
            Cost = cost;
            Id = id;
        }
    }
}