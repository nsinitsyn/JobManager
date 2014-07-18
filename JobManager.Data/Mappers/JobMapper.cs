using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using JobManager.Data.Domain;
using JobManager.Data.Ioc;
using JobManager.Data.Utilities;

namespace JobManager.Data.Mappers
{
    public class JobMapper : BaseDomainDbMapper<Job, JobDb>
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
    }
}
