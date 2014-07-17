using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JobManager.Data;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Domain;
using JobManager.Data.Mappers;
using JobManager.JobManagerService.Ioc;
using Quartz;

namespace JobManager.JobManagerService.Quartz
{
    public class JobsDistributor : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var jobId = dataMap.GetString("jobId");

            // Взять из базы данные и создать воркер
            var jobRepository = IocContainer.Container.Resolve<IJobRepository>();
            var jobDb = jobRepository.GetById(Guid.Parse(jobId));


            var className = jobDb.ClassName;
            var assembly = Assembly.Load(JobManagerSettings.JobsLibraryAssemblyName);
            var classType = assembly.GetType(className);

            var instanceHandle = Activator.CreateInstance(classType);

            var instance = instanceHandle as JobWorkerBase;
            if (instance == null)
            {
                throw new InvalidOperationException(string.Format("Класс {0} не наследует класс JobWorkerBase", className));
            }
            instance.OnEvent += OnEvent;
            instance.OnEventSync += OnEventSync;

            var worker = new Worker
            {
                Id = Guid.NewGuid(),
                Job = JobMapper.Mapper.DtoToDomain(job),
                Instance = instance,
                OperationContext = OperationContext.Current
            };
            _workers.Add(worker);

            var workerDto = WorkerMapper.Mapper.DomainToDto(worker);

            var task = new Task<TransferData>(() => instance.RunWrap(job.Data.GetData(), workerDto));
            task.ContinueWith(t =>
                                  {
                                      var ex = t.Exception;
                                      if (ex != null)
                                      {
                                          GetWorkerAtId(worker.Id).Completed = true;
                                      }
                                      else
                                      {
                                          var result = t.Result;
                                          OnEvent(null, new JobManagerEventArgs
                                                            {
                                                                EventDto = new JobEventDto
                                                                               {
                                                                                   IsReturnResult = true,
                                                                                   TransferData = result,
                                                                                   Worker = workerDto
                                                                               }
                                                            });
                                          GetWorkerAtId(worker.Id).Completed = true;
                                      }
                                  });

            task.Start();
        }
    }
}
