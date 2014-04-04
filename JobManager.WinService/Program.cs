using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobManager.WinService
{
    static class Program
    {
        private static readonly ServiceCore _serviceCore = new ServiceCore();

        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                var servicesToRun = new ServiceBase[] { new JobManager() };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                Start(args);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                Stop();
            }
        }

        public static void Start(string[] args)
        {
            _serviceCore.Start(args);
        }

        public static void Stop()
        {
            _serviceCore.Stop();
        }
    }
}
