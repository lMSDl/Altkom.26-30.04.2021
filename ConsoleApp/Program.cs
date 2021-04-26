using System;
using ConsoleApp.Delegates;
using ConsoleApp.LambdaExamples;
using DataService;
using DataService.Interfaces;
using Models;

namespace ConsoleApp
{
    class Program
    {
        static IStudentsService StudentsService { get; } = new StudentsService();
        static IService<Educator> EducatorsService { get; } = new Service<Educator>();

        static void Main(string[] args)
        {
            var example = new LinqExamples();
            example.Execute();
        }

        private static void Service()
        {
            Console.WriteLine("Hello World!");

            var student1 = new Student();
            var student2 = new Student("Ewa", "Ewowska") { BirthDate = new DateTime(1985, 4, 22) };
            var educator1 = new Educator() { FirstName = "Damian", LastName = "Damianowski" };
            var educator2 = new Educator() { FirstName = "Damian", LastName = "Damianowski", Address = "Warszawska 12" };

            Person person;
            person = student1;
            person = educator1;

            StudentsService.Create(student1);
            student2 = StudentsService.Create(student2);
            var student3 = new Student() { Address = "Kwiatowa 44" };
            StudentsService.Update(student2.Id, student3);

            foreach (var item in StudentsService.Read())
            {
                Console.WriteLine(item.ToJson());
            }

            EducatorsService.Create(educator1);
            EducatorsService.Create(educator2);
            foreach (var item in EducatorsService.Read())
            {
                Console.WriteLine(item.ToJson());
            }
        }
    }
}
