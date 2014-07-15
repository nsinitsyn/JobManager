using System;
using System.Collections.Generic;
using System.Configuration;
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
using JobManager.Data.Domain;

namespace JobManager.JobManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly string _jobsLibraryAssemblyName;
        private const string JobsLibraryAssemblyNameKey = "JobsLibraryAssemblyName";
        private readonly List<Worker> _workers = new List<Worker>();
        private Timer _timer;

        public JobManagerService()
        {
            _jobsLibraryAssemblyName = ConfigurationManager.AppSettings[JobsLibraryAssemblyNameKey];

            _timer = new Timer(TimerTick, null, 0, 1000);

            // Получить все самозапускающиеся джобы из конфига
            // Получить динамически назначенные джобы из базы
        }

        public WorkerDto RunJob(JobDto job)
        {
            var className = job.ClassName;
            var assembly = Assembly.Load(_jobsLibraryAssemblyName);
            var classType = assembly.GetType(className);

            var instanceHandle = Activator.CreateInstance(classType);

            var instance = instanceHandle as JobWorkerBase;
            if (instance == null)
            {
                throw new InvalidOperationException(string.Format("Класс {0} не наследует класс JobWorkerBase", className));
            }
            instance.OnEvent += OnEvent;

            var worker = new Worker
                             {
                                 Id = Guid.NewGuid(),
                                 Job = null,
                                 Instance = instance,
                                 OperationContext = OperationContext.Current
                             };
            _workers.Add(worker);
            job.Id = Guid.NewGuid();

            var workerDto = new WorkerDto
                                {
                                    Id = worker.Id,
                                    JobDto = job
                                };

            var task = new Task<TransferData>(() => instance.RunWrap(job.Data.GetData(), workerDto));
            task.ContinueWith(t =>
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
                                  });
            task.Start();

            return workerDto;

            // !!! Нужно гарантировать удаление воркера из _workers при завершении и исключении
            // !!! Кидать событие при Current = null

            //try
            //{
            //    return workerDto;
            //}
            //catch
            //{
            //    // ?? Надо уведомить об исключении клиента
            //    // ...
            //    return null;
            //}
            //finally
            //{
            //    _workers.Remove(worker);
            //}
        }

        public TransferData Signal(WorkerDto workerDto, TransferData data)
        {
            // ?? ПРОБЛЕМА: Что если Run завершился раньше функции Signal ?

            var worker = GetWorkerAtId(workerDto.Id);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            //var worker = _workers[0];

            var instance = worker.Instance;

            try
            {
                return instance.SignalWrap(data.GetData());
            }
            catch
            {
                // ??? Надо бы уведомить клиента о необработанном исключении и закрыть воркер
                //_workers.Remove(worker);
                return null;
            }
        }

        public void RegisterJob(JobDto job)
        {
            
        }

        private void TimerTick(object state)
        {
            // поскольку джоба запустилась сама, то ей некуда слать события
            var worker = new Worker();
            //var s = worker.EventSubscribers[0];
        }

        private void OnEvent(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.EventDto.Worker.Id;

            var worker = GetWorkerAtId(workerId);
            if (worker == null)
            {
                throw new ArgumentException("Worker not found");
            }

            worker.OperationContext.GetCallbackChannel<IJobManagerServiceCallback>().OnEvent(eventArgs.EventDto);

            //Callback.OnEvent(eventArgs.EventDto);
        }

        private Worker GetWorkerAtId(Guid workerId)
        {
            var worker = _workers.SingleOrDefault(w => w.Id == workerId);
            return worker;
        }

        //private IJobManagerServiceCallback Callback
        //{
        //    get
        //    {
        //        // здесь нужно выбрать контекст по воркеру
        //        return _operationContext.GetCallbackChannel<IJobManagerServiceCallback>();
        //    }
        //}
    }
}
