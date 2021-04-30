using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServices.Core
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        private const string _filename = "c:\\logs\\CoreServiceLog.txt";
        private void Log(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filename));
            File.AppendAllText(_filename, $"{DateTime.Now.ToString()}: {message}\n");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log("OnStart");

            var task = Task.Run(async () =>
            {
                var counter = 0;
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000);
                    Log((counter++).ToString());
                }
            }, stoppingToken);


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
            Log("StopRequested");

            task.Wait();

            Log("OnStop");
        }
    }
}
