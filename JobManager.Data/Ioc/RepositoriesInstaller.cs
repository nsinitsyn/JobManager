using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JobManager.Data.Database.Repositories;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;

namespace JobManager.Data.Ioc
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IJobRepository>().ImplementedBy<JobRepository>().LifestyleTransient());
        }
    }
}
