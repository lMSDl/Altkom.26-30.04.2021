using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsServices.Microsoft
{
    public class LoggingService : ServiceBase
    {
        private const string _filename = "c:\\logs\\MicrosoftServiceLog.txt";
        private void Log(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filename));
            File.AppendAllText(_filename, $"{DateTime.Now.ToString()}: {message}\n");
        }

        private CancellationTokenSource _cancellationTokenSource;

        protected override void OnStart(string[] args)
        {
            Log(nameof(OnStart));
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                var counter = 0;
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Log((counter++).ToString());
                    await Task.Delay(1000);
                }
            }, _cancellationTokenSource.Token);

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            Log(nameof(OnStop));
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            base.OnStop();
        }

        protected override void OnPause()
        {
            Log(nameof(OnPause));
            base.OnPause();
        }

        protected override void OnShutdown()
        {
            Log(nameof(OnShutdown));
            base.OnShutdown();
        }
    }
}
