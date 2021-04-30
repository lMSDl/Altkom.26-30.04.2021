using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace WindowsServices.TopShelf
{
    public class LoggingService : ServiceControl
    {
        private const string _filename = "c:\\logs\\TopshelfServiceLog.txt";
        private void Log(string message)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filename));
            File.AppendAllText(_filename, $"{DateTime.Now.ToString()}: {message}\n");
        }

        private CancellationTokenSource _cancellationTokenSource;

        public bool Start(HostControl hostControl)
        {
            Log(nameof(Start));
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                var counter = 0;
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (counter == 10)
                        Loop();
                    Log((counter++).ToString());
                    await Task.Delay(1000);
                }
            }, _cancellationTokenSource.Token);

            return true;
        }

        public void Loop()
        {
            Loop();
        }

        public bool Stop(HostControl hostControl)
        {
            Log(nameof(Stop));
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            return true;
        }
    }
}
