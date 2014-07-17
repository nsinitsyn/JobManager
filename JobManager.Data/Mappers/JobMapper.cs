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
            throw new NotImplementedException();
        }

        public override JobDb DomainToDb(Job job)
        {
            throw new NotImplementedException();
        }

        public override JobDto DomainToDto(Job job)
        {
            throw new NotImplementedException();
        }

        public override JobDb DtoToDb(JobDto jobDto)
        {
            var sr = new StreamReader(jobDto.Data.SerializedData);
            var dataString = sr.ReadToEnd();
            var jobDb = new JobDb
                            {
                                Id = jobDto.Id,
                                ClassName = jobDto.ClassName,
                                Data = dataString,
                                Triggers = jobDto.Triggers.Select(TriggerMapper.Mapper.DtoToDb).ToList()
                            };

            return jobDb;
        }

        public override Job DtoToDomain(JobDto dto)
        {
            var job = new Job
                          {
                              Id = dto.Id,
                              ClassName = dto.ClassName,
                              Data = dto.Data.GetData(),
                              Triggers = dto.Triggers.Select(TriggerMapper.Mapper.DtoToDomain).ToList()
                          };

            return job;
        }
    }
}
