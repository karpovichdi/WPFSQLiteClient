namespace Turtur.Models
{
    public class Employe
    {
        public string FirstName;
        public string LastName;
        public readonly int Id;

        public string FIRSTNAME
        {
            get => FirstName;
            set => FirstName = value;
        }

        public string LASTNAME
        {
            get => LastName;
            set => LastName = value;
        }

        public Employe(int id, string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }
    }
}