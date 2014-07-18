using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using JobManager.Data.Domain;
using JobManager.Data.Ioc;
using JobManager.Data.Mappers;
using Quartz;
using Quartz.Impl;
using QuartzLib = Quartz;

namespace JobManager.Data.Business
{
    // Singleton
    public class JobRunner
    {
        public event JobManagerEventHandler OnEvent;
        public event JobManagerEventSyncHandler OnEventSync;

        private static JobRunner _runner;

        public static JobRunner Runner
        {
            get { return _runner ?? (_runner = new JobRunner()); }
        }

        private readonly List<Worker> _workers = new List<Worker>();
        private readonly IScheduler _scheduler;
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        private JobRunner()
        {
            _jobRepository = IocContainer.Container.Resolve<IJobRepository>();
            _unitOfWork = IocContainer.Container.Resolve<IUnitOfWork>();

            ISchedulerFactory schedFact = new StdSchedulerFactory();
            _scheduler = schedFact.GetScheduler();
            _scheduler.Start();

            // Получить все самозапускающиеся джобы из конфига
            // Получить зарегистрированные джобы из базы
        }

        public Worker RunJob(Job job)
        {
            var className = job.ClassName;
            var assembly = Assembly.Load(JobManagerSettings.JobsLibraryAssemblyName);
            var classType = assembly.GetType(className);

            var instanceHandle = Activator.CreateInstance(classType);

            var instance = instanceHandle as JobWorkerBase;
            if (instance == null)
            {
                throw new InvalidOperationException(string.Format("Класс {0} не наследует класс JobWorkerBase", className));
            }
            instance.OnEvent += OnEventHandler;
            instance.OnEventSync += OnEventSyncHandler;

            var worker = new Worker
            {
                Id = Guid.NewGuid(),
                Job = job,
                Instance = instance
            };
            _workers.Add(worker);

            var task = new Task<object>(() => instance.RunWrap(job.Data, worker));
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
                        Event = new JobEvent
                        {
                            IsReturnResult = true,
                            Data = result,
                            WorkerId = worker.Id
                        }
                    });
                    GetWorkerAtId(worker.Id).Completed = true;
                }
            });

            task.Start();

            return worker;
        }

        public object Signal(Guid workerId, object data)
        {
            // ?? ПРОБЛЕМА: Что если Run завершился раньше функции Signal ?
            // ?? Что если слать сигнал после получения returnResult через событие

            var worker = GetWorkerAtId(workerId);
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
                return instance.SignalWrap(data);
            }
            catch
            {
                // ??? Надо бы уведомить клиента о необработанном исключении и закрыть воркер
                // GetWorkerAtId(worker.Id).Completed = true;
                return null;
            }
        }

        public Job GetJob(Guid jobId)
        {
            var job = GetJobAtId(jobId);
            return job;
        }

        // Если джоба уже зашедулена, то exception (вначале нужно расшедулить ее)
        // Если джобы нет в базе, она создается и шедулится
        // Если джоба есть в базе, ее поля обновляются. Затем она шедулится
        public Guid ScheduleJob(Job job)
        {
            if (ScheduledJob(job.Id))
            {
                throw new InvalidOperationException("This job has been already scheduled");
            }

            Job scheduleJob;

            var jobExistDb = GetJobDbAtId(job.Id);
            var jobDb = JobMapper.Mapper.DomainToDb(job);

            if (jobExistDb == null)
            {
                _jobRepository.Add(jobDb);
                _unitOfWork.Commit();
                scheduleJob = job;
            }
            else
            {
                // Change job info
                jobExistDb.ClassName = jobDb.ClassName;
                jobExistDb.Data = jobDb.Data;
                jobExistDb.Triggers = jobDb.Triggers;
                _jobRepository.Update(jobExistDb);
                _unitOfWork.Commit();
                scheduleJob = JobMapper.Mapper.DbToDomain(jobExistDb);
            }

            QuartzScheduleJob(scheduleJob);
            return scheduleJob.Id;
        }

        public void RescheduleJob(Job job)
        {
        }

        public bool UnscheduleJob(Guid jobId)
        {
            var job = GetJobAtId(jobId);
            if (job == null)
            {
                throw new InvalidOperationException("Job not found");
            }

            var deleteResult = _scheduler.DeleteJob(JobKey(jobId));
            return deleteResult;
        }

        public void DeleteJob(Guid jobId)
        {
            throw new NotImplementedException();
        }

        public bool HasJobScheduled(Guid jobId)
        {
            var result = ScheduledJob(jobId);
            return result;
        }

        private void OnEventHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.Event.WorkerId;
            var worker = GetWorkerAtId(workerId);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            OnEvent(this, eventArgs);
        }

        private object OnEventSyncHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.Event.WorkerId;
            var worker = GetWorkerAtId(workerId);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            var eventResult = OnEventSync(this, eventArgs);
            return eventResult;
        }

        private Worker GetWorkerAtId(Guid workerId)
        {
            var worker = _workers.SingleOrDefault(w => w.Id == workerId);
            return worker;
        }

        private Job GetJobAtId(Guid jobId)
        {
            var jobDb = GetJobDbAtId(jobId);
            var job = JobMapper.Mapper.DbToDomain(jobDb);
            return job;
        }

        private JobDb GetJobDbAtId(Guid jobId)
        {
            var jobDb = _jobRepository.GetById(jobId);
            return jobDb;
        }

        private JobKey JobKey(Guid jobId)
        {
            return new JobKey(jobId.ToString());
        }

        private bool ScheduledJob(Guid jobId)
        {
            var result = _scheduler.CheckExists(JobKey(jobId));
            return result;
        }

        private void QuartzScheduleJob(Job job)
        {
            IDictionary jobDataDictionary = new Dictionary<string, object> { { "jobId", job.Id.ToString() } };

            var jobDetail = JobBuilder.Create<JobsDistributor>()
                .WithIdentity(job.Id.ToString())
                .SetJobData(new JobDataMap(jobDataDictionary))
                .Build();

            var triggers = new QuartzLib.Collection.HashSet<ITrigger>();

            foreach (var jobTrigger in job.Triggers)
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(Guid.NewGuid().ToString())
                    .WithCronSchedule(jobTrigger.Cron)
                    .Build();

                triggers.Add(trigger);
            }

            _scheduler.ScheduleJob(jobDetail, triggers, true);
        }
    }
}
