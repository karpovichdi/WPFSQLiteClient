namespace Turtur.Models
{
    public class Car
    {
        public readonly int Id;
        public string Name;
        public string Photo;
        public string StateNumber;

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

        public string STATENUMBER
        {
            get => StateNumber;
            set => StateNumber = value;
        }

        public Car(int id, string name, string photo, string stateNumber)
        {
            Id = id;
            Name = name;
            Photo = photo;
            StateNumber = stateNumber;
        }
    }
}