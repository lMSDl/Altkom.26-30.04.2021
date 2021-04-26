using System;
using DataService;
using DataService.Interfaces;
using Models;

namespace ConsoleApp
{
    class Program
    {
        static IStudentsService Service { get; } = new StudentsService();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var student1 = new Student();
            var student2 = new Student("Ewa", "Ewowska") { BirthDate = new DateTime(1985, 4, 22) };
            var educator1 = new Educator() { FirstName = "Damian", LastName = "Damianowski" };
            var educator2 = new Educator() { FirstName = "Damian", LastName = "Damianowski", Address = "Warszawska 12" };

            Person person;
            person = student1;
            person = educator1;
            
            Service.Create(student1);
            student2 = Service.Create(student2);
            var student3 = new Student() { Address = "Kwiatowa 44" };
            Service.Update(student2.Id, student3);

            foreach (var item in Service.Read())
            {
                Console.WriteLine(item.ToJson());
            }
        }
    }
}
