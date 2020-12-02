using System;
using System.Collections.Generic;
using System.Text;

namespace Turtur
{
    class Employe
    {
        private string firstName, lastName;

        private int id;

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public Employe() { }

        public Employe(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }
}
