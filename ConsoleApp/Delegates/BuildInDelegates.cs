using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class BuildInDelegates
    {
        public EventHandler OddNumberEvent;
        public bool Substract(int a, int b)
        {
            var result = a - b;
            Console.WriteLine(result);
            return result % 2 != 0;
        }

        public void Add(int a, int b)
        {
            var result = a + b;
            Console.WriteLine(result);
            if (result % 2 != 0)
                OddNumberEvent?.Invoke(this, EventArgs.Empty);
        }

        private int _counter = 0;
        private void CountOddNumbers()
        {
            _counter++;
        }

        public void Execute()
        {
            OddNumberEvent += Count_OddNumberEvent;

            Method(Add, Substract);
        }

        //public delegate void Method1Delegate(int a, int b);
        //public delegate bool Method2Delegate(int a, int b);
        //private void Method(Method1Delegate method1, Method2Delegate method2)
        private void Method(Action<int, int> method1, Func<int, int, bool> method2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    method1(i, ii);
                    if (method2(i, ii))
                        OddNumberEvent?.Invoke(this, EventArgs.Empty);
                }
            }
            Console.WriteLine($"Counter: " + _counter);
        }

        private void Count_OddNumberEvent(object sender, EventArgs args)
        {
            CountOddNumbers();
        }
    }
}
