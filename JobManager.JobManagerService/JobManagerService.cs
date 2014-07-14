using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JobManager.Data;
using JobManager.Data.Business;
using JobManager.Data.DTO;

namespace JobManager.JobManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly string _jobsLibraryAssemblyName;
        private const string JobsLibraryAssemblyNameKey = "JobsLibraryAssemblyName";
        private readonly Scheduler.Scheduler _scheduler;

        public JobManagerService()
        {
            //_scheduler = new Scheduler.Scheduler();
            _jobsLibraryAssemblyName = ConfigurationManager.AppSettings[JobsLibraryAssemblyNameKey];

            // Получить все самозапускающиеся джобы из конфига
            // Получить динамически назначенные джобы из базы

            //_scheduler.Start();
            //_scheduler.DeleteJob(jobFromConfig);
            // Зашедулить снова джобы из конфига
        }

        public JobOutputDataBase RunJob(JobInputDataBase jobInputData)
        {
            var className = jobInputData.JobWorkerClassName;
            var assembly = Assembly.Load(_jobsLibraryAssemblyName);
            var classType = assembly.GetType(className);

            var instanceHandle = Activator.CreateInstance(classType);

            var instance = instanceHandle as JobWorkerBase;
            if (instance == null)
            {
                throw new InvalidOperationException(string.Format("Класс {0} не реализует протокол IJobWorkerBase", className));
            }
            instance.OnEvent += OnEvent;
            return instance.Run(jobInputData);
        }

        private void OnEvent(object sender, JobManagerEventArgs eventArgs)
        {
            Callback.OnEvent(eventArgs.EventData);
        }

        private IJobManagerServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IJobManagerServiceCallback>();
            }
        }
    }
}
