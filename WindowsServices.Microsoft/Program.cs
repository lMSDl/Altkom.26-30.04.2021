using System;
using System.ServiceProcess;

namespace WindowsServices.Microsoft
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase.Run(new LoggingService());
        }
    }
}
