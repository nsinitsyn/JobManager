using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Domain;
using JobManager.Data.Mappers;

namespace JobManager.JobManagerService.Business
{
    public class JobRunner
    {
        private JobRunner _runner;

        public JobRunner Runner
        {
            get { return _runner ?? (_runner = new JobRunner()); }
        }

        private readonly List<Worker> _workers = new List<Worker>();

        public Worker RunJob(Job job)
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
                Job = job,
                Instance = instance,
                OperationContext = OperationContext.Current
            };
            _workers.Add(worker);

            var task = new Task<TransferData>(() => instance.RunWrap(job.Data, worker));
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

            return worker;
        }

        private void OnEvent(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.EventDto.Worker.Id;
            var worker = GetWorkerAtId(workerId);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            GetCallbackAtWorker(worker).OnEvent(eventArgs.EventDto);
        }

        private TransferData OnEventSync(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.EventDto.Worker.Id;
            var worker = GetWorkerAtId(workerId);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            var eventResult = GetCallbackAtWorker(worker).OnEventSync(eventArgs.EventDto);
            return eventResult;
        }

        private IJobManagerServiceCallback GetCallbackAtWorker(Worker worker)
        {
            var context = worker.OperationContext.GetCallbackChannel<IJobManagerServiceCallback>();
            return context;
        }

        private Worker GetWorkerAtId(Guid workerId)
        {
            var worker = _workers.SingleOrDefault(w => w.Id == workerId);
            return worker;
        }
    }
}
