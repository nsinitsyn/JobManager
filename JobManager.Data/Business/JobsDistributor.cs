using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Ioc;
using JobManager.Data.Mappers;
using Quartz;

namespace JobManager.Data.Business
{
    public class JobsDistributor : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var jobId = dataMap.GetString("jobId");

            var jobRepository = IocContainer.Container.Resolve<IJobRepository>();
            var jobDb = jobRepository.GetById(Guid.Parse(jobId));

            var job = JobMapper.Mapper.DbToDomain(jobDb);
            JobRunner.Runner.RunJob(job);
        }
    }
}
