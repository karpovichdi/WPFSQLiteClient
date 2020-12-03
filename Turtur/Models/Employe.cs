namespace Turtur.Models
{
    public class Employe
    {
        public readonly string FirstName;
        public readonly string LastName;
        public readonly int Id;
        
        public Employe(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }
    }
}