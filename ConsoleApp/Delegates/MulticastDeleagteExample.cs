using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class MulticastDeleagteExample
    {
        public delegate void ShowMessage(string @string);

        public void Message1(string msg)
        {
            Console.WriteLine($"1st message: {msg}");
        }
        public void Message2(string msg)
        {
            Console.WriteLine($"2nd message: {msg}");
        }
        public void Message3(string msg)
        {
            Console.WriteLine($"3rd message: {msg}");
        }

        public void Execute()
        {
            ShowMessage showMessage = null;

            showMessage += Message1;
            showMessage += Message2;
            showMessage += Message3;
            showMessage("Hello!");

            showMessage -= Message2;
            showMessage("Hello without 2!");

            showMessage = Message2;
            showMessage("Hello 2!");

            showMessage += Console.WriteLine;
            showMessage("Hello!");
        }
    }
}
