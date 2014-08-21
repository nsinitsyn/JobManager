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
using JobManager.Business;
using JobManager.Business.Events;
using JobManager.Data;
using JobManager.JobManagerService.Mappers;
using JobManager.Transfer.Data.DTO;
using JobEventMapper = JobManager.JobManagerService.Mappers.JobEventDtoMapper;

namespace JobManager.JobManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly Dictionary<Guid, OperationContext> _workerContexts = new Dictionary<Guid, OperationContext>();
        private readonly Dictionary<Guid, OperationContext> _eventSubscribers = new Dictionary<Guid, OperationContext>();

        public JobManagerService()
        {
            JobRunner.Runner.OnEvent += OnEventHandler;
            JobRunner.Runner.OnEventSync += OnEventSyncHandler;
            JobRunner.Runner.OnWorkerWillBeStarted += OnWorkerWillBeStartedHandler;
        }

        public WorkerDto RunJob(JobDto jobDto)
        {
            if (jobDto.Id == Guid.Empty)
            {
                jobDto.Id = Guid.NewGuid();
            }
            var job = JobDtoMapper.Mapper.DtoToDomain(jobDto);
            var worker = JobRunner.Runner.RunJob(job);
            _workerContexts[worker.Id] = OperationContext.Current;
            return WorkerDtoMapper.Mapper.DomainToDto(worker);
        }

        public TransferData Signal(Guid workerId, TransferData data)
        {
            var resultData = JobRunner.Runner.Signal(workerId, data.GetData());
            return new TransferData(resultData);
        }

        public JobDto GetJob(Guid jobId)
        {
            var job = JobRunner.Runner.GetJob(jobId);
            var jobDto = JobDtoMapper.Mapper.DomainToDto(job);
            return jobDto;
        }

        public List<WorkerDto> GetWorkers()
        {
            var workers = JobRunner.Runner.GetWorkers();
            var workerDtos = workers.Select(WorkerDtoMapper.Mapper.DomainToDto).ToList();
            return workerDtos;
        }

        public Guid ScheduleJob(JobDto jobDto)
        {
            if (jobDto.Id == Guid.Empty)
            {
                jobDto.Id = Guid.NewGuid();
            }
            var job = JobDtoMapper.Mapper.DtoToDomain(jobDto);
            var jobId = JobRunner.Runner.ScheduleJob(job);
            return jobId;
        }

        public void RescheduleJob(JobDto jobDto)
        {
            var job = JobDtoMapper.Mapper.DtoToDomain(jobDto);
            JobRunner.Runner.RescheduleJob(job);
        }
        public void UnscheduleJob(Guid jobId)
        {
            JobRunner.Runner.UnscheduleJob(jobId);
        }

        public void DeleteJob(Guid jobId)
        {
            JobRunner.Runner.DeleteJob(jobId);
        }

        public void SubscribeClientContext()
        {
            var currentContext = OperationContext.Current;
            var curContextId = Callback(currentContext).ClientIdentifier();
            if (!_eventSubscribers.ContainsKey(curContextId))
            {
                _eventSubscribers[curContextId] = currentContext;
            }
            else
            {
                throw new InvalidOperationException("This client has been already subscribed");
            }
        }

        public void UnsubscribeClientContext()
        {
            var currentContext = OperationContext.Current;
            var curContextId = Callback(currentContext).ClientIdentifier();
            if (_eventSubscribers.ContainsKey(curContextId))
            {
                _eventSubscribers.Remove(curContextId);
            }
            else
            {
                throw new InvalidOperationException("This client is not subscribed");
            }
        }

        public void SetClientContextToWorker(Guid workerId)
        {
            if (_workerContexts[workerId] != null)
            {
                throw new InvalidOperationException("This worker has been already has client context");
            }
            _workerContexts[workerId] = OperationContext.Current;
        }

        private IJobManagerServiceCallback Callback(OperationContext context)
        {
            return context.GetCallbackChannel<IJobManagerServiceCallback>();
        }

        private void OnEventHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var eventDto = JobEventDtoMapper.Mapper.DomainToDto(eventArgs.Event);

            var context = _workerContexts[eventDto.WorkerId];
            // Если context не найден, то сообщить об этом воркеру
            Callback(context).OnEvent(eventDto);
        }

        private object OnEventSyncHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var eventDto = JobEventDtoMapper.Mapper.DomainToDto(eventArgs.Event);

            var context = _workerContexts[eventDto.WorkerId];
            var result = Callback(context).OnEventSync(eventDto);
            return result;
        }

        private void OnWorkerWillBeStartedHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var workerId = eventArgs.Event.WorkerId;
            var worker = JobRunner.Runner.GetWorkers().Single(w => w.Id == workerId);
            var workerDto = WorkerDtoMapper.Mapper.DomainToDto(worker);

            foreach (var context in _eventSubscribers.Select(x => x.Value))
            {
                Callback(context).WorkerWillBeStarted(workerDto);
            }

            //DefaultContext.GetCallbackChannel<IJobManagerServiceCallback>().OnEventSync(eventDto);
        }
    }
}
