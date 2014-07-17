using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobManager.Data;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using JobManager.Data.Domain;
using JobManager.Data.Mappers;
using JobManager.JobManagerService.Business;
using JobManager.JobManagerService.Ioc;
using JobManager.JobManagerService.Quartz;
using Quartz;
using Quartz.Impl;
using QuartzLib = Quartz;

namespace JobManager.JobManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly JobRunner _runner;

        public JobManagerService()
        {
            _runner = new JobRunner();
        }

        public WorkerDto RunJob(JobDto job)
        {
            if (job.Id == Guid.Empty)
            {
                job.Id = Guid.NewGuid();
            }

            var className = job.ClassName;
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

            return workerDto;
        }

        public TransferData Signal(WorkerDto workerDto, TransferData data)
        {
            // ?? ПРОБЛЕМА: Что если Run завершился раньше функции Signal ?
            // ?? Что если слать сигнал после получения returnResult через событие

            var worker = GetWorkerAtId(workerDto.Id);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }
            if (worker.Completed)
            {
                throw new InvalidOperationException("Worker has been completed");
            }

            var instance = worker.Instance;

            try
            {
                return instance.SignalWrap(data.GetData());
            }
            catch
            {
                // ??? Надо бы уведомить клиента о необработанном исключении и закрыть воркер
                // GetWorkerAtId(worker.Id).Completed = true;
                return null;
            }
        }

        public Guid RegisterJob(JobDto jobDto)
        {
            var job = JobMapper.Mapper.DtoToDomain(jobDto);
            var jobId = _runner.RegisterJob(job);

            //if (job.Id == Guid.Empty)
            //{
            //    job.Id = Guid.NewGuid();
            //}

            //var jobRepository = IocContainer.Container.Resolve<IJobRepository>();
            //var unitOfWork = IocContainer.Container.Resolve<IUnitOfWork>();
            //var jobDb = JobMapper.Mapper.DtoToDb(job);
            //jobRepository.Add(jobDb);
            //unitOfWork.Commit();

            //IDictionary jobDataDictionary = new Dictionary<string, object> { { "jobId", job.Id.ToString() }};

            //var jobDetail = JobBuilder.Create<JobsDistributor>()
            //    .WithIdentity(Guid.NewGuid().ToString())
            //    .SetJobData(new JobDataMap(jobDataDictionary))
            //    .Build();

            //var triggers = new QuartzLib.Collection.HashSet<ITrigger>();

            //foreach (var jobTrigger in job.Triggers)
            //{
            //    var trigger = TriggerBuilder.Create()
            //        .WithIdentity(Guid.NewGuid().ToString())
            //        .WithCronSchedule(jobTrigger.Cron)
            //        .Build();

            //    triggers.Add(trigger);
            //}

            //_scheduler.ScheduleJob(jobDetail, triggers, true);

            //return job.Id;
        }

        //private void OnEvent(object sender, JobManagerEventArgs eventArgs)
        //{
        //    var workerId = eventArgs.EventDto.Worker.Id;
        //    var worker = GetWorkerAtId(workerId);
        //    if (worker == null)
        //    {
        //        throw new ArgumentException("Worker not found");
        //    }

        //    GetCallbackAtWorker(worker).OnEvent(eventArgs.EventDto);
        //}

        //private TransferData OnEventSync(object sender, JobManagerEventArgs eventArgs)
        //{
        //    var workerId = eventArgs.EventDto.Worker.Id;
        //    var worker = GetWorkerAtId(workerId);
        //    if (worker == null)
        //    {
        //        throw new ArgumentException("Worker not found");
        //    }

        //    var eventResult = GetCallbackAtWorker(worker).OnEventSync(eventArgs.EventDto);
        //    return eventResult;
        //}

        //private IJobManagerServiceCallback GetCallbackAtWorker(Worker worker)
        //{
        //    var context = worker.OperationContext.GetCallbackChannel<IJobManagerServiceCallback>();
        //    return context;
        //}

        //private Worker GetWorkerAtId(Guid workerId)
        //{
        //    var worker = _workers.SingleOrDefault(w => w.Id == workerId);
        //    return worker;
        //}
    }
}
