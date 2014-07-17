using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace JobManager.Data.Ioc
{
    public static class IocContainer
    {
        static IocContainer()
        {
            Container = new WindsorContainer().Install(FromAssembly.InThisApplication());
        }

        public static IWindsorContainer Container { get; set; }
    }
}
