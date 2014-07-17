using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace JobManager.DbInitializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JobManager Database Initializer\nAvailable commands:\n0 - delete all data from database\n1 - fill database with test data\n2 - exit");

            var iocContainer = new WindsorContainer().Install(
                FromAssembly.Named("JobManager.Data"));

            iocContainer.Register(Component.For<DbWorker>());
            var worker = iocContainer.Resolve<DbWorker>();

            while (true)
            {
                Console.Write(">");
                string cmd = Console.ReadLine();
                int command;
                bool exit = false;
                if (Int32.TryParse(cmd, out command))
                {
                    switch (command)
                    {
                        case 0:
                            if (!worker.DeleteAllDataFromDb())
                            {
                                Console.WriteLine("data hasn't been deleted");
                            }
                            break;

                        case 1:
                            if (worker.DeleteAllDataFromDb())
                            {
                                worker.FillDbWithTestData();
                            }
                            else
                            {
                                Console.WriteLine("data hasn't been deleted");
                            }
                            break;

                        case 2:
                            exit = true;
                            break;

                        default:
                            break;
                    }
                }

                if (exit)
                {
                    break;
                }
            }
        }
    }
}
