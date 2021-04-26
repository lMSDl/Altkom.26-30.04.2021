using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{
    public class ConsoleService : IWriteService
    {
        public void WriteLine(string @string)
        {
            Console.WriteLine(@string);
        }
    }
}
