using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using JobManager.Data.Domain;
using JobManager.Data.Ioc;
using JobManager.Data.Utilities;

namespace JobManager.Data.Mappers
{
    public class JobMapper : BaseMapper<JobDto, Job, JobDb>
    {
        static JobMapper()
        {
            Mapper = new JobMapper();
        }

        public static JobMapper Mapper { get; set; }

        public override Job DbToDomain(JobDb jobDb)
        {
            if (jobDb == null)
            {
                return null;
            }

            var job = new Job
                          {
                              Id = jobDb.Id,
                              ClassName = jobDb.ClassName,
                              Triggers = jobDb.Triggers.Select(TriggerMapper.Mapper.DbToDomain).ToList(),
                              Data = Serializator.DeserializeFromString(jobDb.Data)
                          };
            return job;
        }

        public override JobDb DomainToDb(Job job)
        {
            if (job == null)
            {
                return null;
            }

            var jobDb = new JobDb
                            {
                                Id = job.Id,
                                ClassName = job.ClassName,
                                Data = Serializator.SerializeToString(job.Data),
                                Triggers = job.Triggers.Select(TriggerMapper.Mapper.DomainToDb).ToList()
                            };
            return jobDb;
        }

        public override JobDto DomainToDto(Job job)
        {
            if (job == null)
            {
                return null;
            }

            var jobDto = new JobDto
                             {
                                 Id = job.Id,
                                 ClassName = job.ClassName,
                                 Data = new TransferData(job.Data),
                                 Triggers = job.Triggers.Select(TriggerMapper.Mapper.DomainToDto).ToList(),
                                 Scheduled = job.Scheduled
                             };

            return jobDto;
        }

        public override Job DtoToDomain(JobDto jobDto)
        {
            if (jobDto == null)
            {
                return null;
            }

            var job = new Job
                          {
                              Id = jobDto.Id,
                              ClassName = jobDto.ClassName,
                              Data = jobDto.Data.GetData(),
                              Triggers = jobDto.Triggers.Select(TriggerMapper.Mapper.DtoToDomain).ToList()
                          };

            return job;
        }
    }
}
