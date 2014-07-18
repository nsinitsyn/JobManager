﻿using System;
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
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using JobManager.Data.Domain;
using JobManager.Data.Mappers;
using JobManager.JobManagerService.DTO;
using JobManager.JobManagerService.Mappers;
using JobEventMapper = JobManager.JobManagerService.Mappers.JobEventDtoMapper;

namespace JobManager.JobManagerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobManagerService : IJobManagerService
    {
        private readonly Dictionary<Guid, OperationContext> _workerContexts = new Dictionary<Guid, OperationContext>();

        public JobManagerService()
        {
            JobRunner.Runner.OnEvent += OnEventHandler;
            JobRunner.Runner.OnEventSync += OnEventSyncHandler;
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

        private void OnEventHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var eventDto = JobEventDtoMapper.Mapper.DomainToDto(eventArgs.Event);

            var context = _workerContexts[eventDto.WorkerId];
            context.GetCallbackChannel<IJobManagerServiceCallback>().OnEvent(eventDto);
        }

        private object OnEventSyncHandler(object sender, JobManagerEventArgs eventArgs)
        {
            var eventDto = JobEventDtoMapper.Mapper.DomainToDto(eventArgs.Event);

            var context = _workerContexts[eventDto.WorkerId];
            var result = context.GetCallbackChannel<IJobManagerServiceCallback>().OnEventSync(eventDto);
            return result;
        }
    }
}
