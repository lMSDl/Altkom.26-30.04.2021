using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student : Person
    {
        public Student()
        {

        }

        public Student(string firstName, string lastName, string address, DateTime birthDate) : this(firstName, lastName, address)
        {
            BirthDate = birthDate;
        }
        public Student(string firstName, string lastName, string address) : this(firstName, lastName)
        {
            Address = address;
        }
        public Student(string firstName, string lastName) : this(lastName)
        {
            FirstName = firstName;
        }
        public Student(string lastName)
        {
            LastName = lastName;
        }

        public int AverageGrade { get; set; }
        public int StudyYear {get; set;}
    }
}
