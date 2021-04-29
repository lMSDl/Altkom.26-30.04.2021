﻿using System;
using Topshelf;

namespace WindowsServices.TopShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<LoggingService>();
                x.SetServiceName("TopshelfServiceType");

                x.EnableServiceRecovery(x =>
                {
                    x.RestartService(TimeSpan.FromSeconds(10))
                    .RestartService(TimeSpan.FromSeconds(20))
                    .RestartService(TimeSpan.FromSeconds(30))
                    .SetResetPeriod(1);
                });

                x.RunAsLocalSystem();
                x.StartAutomaticallyDelayed();
            });
        }
    }
}
