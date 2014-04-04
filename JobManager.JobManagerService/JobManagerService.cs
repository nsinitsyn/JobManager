﻿using System;
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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly string _jobsLibraryAssemblyName;
        private const string JobsLibraryAssemblyNameKey = "JobsLibraryAssemblyName";

        public JobManagerService()
        {
            _jobsLibraryAssemblyName = ConfigurationManager.AppSettings[JobsLibraryAssemblyNameKey];
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