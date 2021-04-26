using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class DelegatesExample
    {
        public delegate void NoParametersNoReturnDeleagete();
        public delegate void ParameterNoReturnDeleagete(string @string);
        public delegate bool ParametersReturnDeleagete(int int1, int int2);

        public void Func1()
        {
            Console.WriteLine("1");
        }
        public void Func2(string @string)
        {
            Console.WriteLine(@string);
        }
        public bool Func3(int a, int b)
        {
            //Console.WriteLine("a=" + a + " b=" + b);
            //Console.WriteLine(string.Format("a={0} b={1}", a, b));
            Console.WriteLine($"a={a} b={b}");
            return a == b;
        }

        public ParametersReturnDeleagete Delegate3 { get; set; }

        public void Execute()
        {
            var delegate1 = new NoParametersNoReturnDeleagete(Func1);
            delegate1();

            ParameterNoReturnDeleagete delegate2 = null;
            //if(delegate2 != null)
            //    delegate2.Invoke("2");
            delegate2?.Invoke("2");
            delegate2 = Func2;
            delegate2?.Invoke("2");


            Delegate3 = Func3;

            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    if(Delegate3(i, ii))
                        Console.WriteLine("==");
                }
            }
        }
    }
}
