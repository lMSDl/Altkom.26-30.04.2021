using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.SOLID.I
{
    internal interface IExcelFormatter
    {
        void ToExcel();
    }
    internal interface IPdfFormatter
    { 
        void ToPdf();
    }

    class Report : IExcelFormatter, IPdfFormatter
    {
        public void ToExcel()
        {
            Console.WriteLine("Excel generated");
        }

        public void ToPdf()
        {
            Console.WriteLine("Pdf generated");
        }
    }

    class Poem : IPdfFormatter
    {
        public void ToPdf()
        {
            Console.WriteLine("Pdf generated");
        }
    }
}
