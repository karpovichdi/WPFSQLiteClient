namespace Turtur.Models
{
    public class Employe
    {
        public readonly string FirstName;
        public readonly string LastName;
        public readonly int Id;
        
        public Employe(int id, string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }
    }
}