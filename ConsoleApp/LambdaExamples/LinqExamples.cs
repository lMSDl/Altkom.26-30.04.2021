using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.LambdaExamples
{
    public class LinqExamples
    {

        int[] numbers = new[] { 1, 3, 4, 2, 5, 7, 8, 6, 9, 0 };
        List<string> strings = "wlazł kotek na płotek i mruga".Split(' ').ToList();

        List<Person> students = new List<Person>
        {
            new Student { FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Ewa", LastName = "Ewowska", BirthDate = new DateTime(2000, 1, 1)  } ,
            new Student { FirstName = "Adam", LastName = "Ewowska", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Ewa", LastName = "Adamska", BirthDate = new DateTime(1994, 1, 1)  } ,
            new Student { FirstName = "Piotr", LastName = "Adamski", BirthDate = new DateTime(1978, 2, 21) },
            new Student { FirstName = "Kamila", LastName = "Ewowska", BirthDate = new DateTime(1934, 1, 1)  } ,
    };

        public void Execute()
        {
            var queryResult1 = from item in numbers where item > 4 select item;
            var queryResult2 = from item in numbers where item < 2 || item > 8 select item;

            var queryResult3 = numbers.Where(item => item > 4).ToList();
            var queryResult4 = numbers.Where(item => item > 4).OrderBy(x => x).ToList();
            var queryResult5 = numbers.Where(item => item > 4).OrderByDescending(x => x).ToList();
            var queryResult6 = numbers.Where(item => item > 4).Where(item => item % 2 == 0).ToList();

            var queryResult7 = strings.Where(x => x.Length == 5).Select(x => x.ToUpper()).ToList();
            var queryResult8 = strings.Select(x => x.Length).Where(x => x != 5).ToList();

            var queryResult9 = students
                .Where(x => x.LastName.Contains("ski", StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.BirthDate)
                .Select(x => $"{x.FirstName} + { x.LastName}")
                .ToList();

            var queryResult10 = students
                .Where(x => x.LastName.Contains("ski", StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(x => x.BirthDate)
                .Select(x =>
                {
                    var result = $"{x.FirstName} + { x.LastName}";
                    return result;
                })
                .Aggregate("Osoby: ",(a, b) => $"{a} {b}");
        }

    }
}
