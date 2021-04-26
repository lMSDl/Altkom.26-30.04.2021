using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.LambdaExamples
{
    public class LambdaExamples
    {
        Func<int, int, int> Calculator { get; set; }
        Action<int> SomeAction { get; set; }
        Action AnotherAction { get; set; }

        public void Execute()
        {
            Calculator += //delegate (int a, int b) { return a + b; };
                          //(a, b) => { return a + b; };
                            (a, b) => a + b;

            SomeAction += //(param) => Console.WriteLine(param);
                            param => Console.WriteLine(param);

            AnotherAction += () => Console.WriteLine("Hello!");

            SomeMethod(x => Console.WriteLine(x), "Hello!");
        }

        void SomeMethod(Action<string> stringAction, string @string)
        {
            stringAction(@string);
        }
    }
}
