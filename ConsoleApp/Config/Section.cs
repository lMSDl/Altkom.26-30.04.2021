using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Config
{
    public class Section
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public Section Subsection { get; set; }
    }
}
