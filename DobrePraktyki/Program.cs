using DobrePraktyki.SOLID.L;
using System;

namespace DobrePraktyki
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 3;

            Console.WriteLine($"{a}*{b}=");

            Rectangle rectangle = new Square();
            rectangle.A = a;
            rectangle.B = b;


            PrintArea(rectangle);


            Console.WriteLine("Hello World!");
        }

        public static void PrintArea(Rectangle shape)
        {
            Console.WriteLine(shape.Area);
        }
    }
}
