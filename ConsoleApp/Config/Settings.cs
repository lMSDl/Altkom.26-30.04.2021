using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Config
{
    public class Settings
    {
        public string HelloJson { get; set; }
        public string HelloIni { get; set; }
        public string Data { get; set; }

        public Section Section { get; set; }
    }
}
