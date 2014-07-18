using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JobManager.Data;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Domain;
using JobManager.Data.Mappers;
using JobManager.JobManagerService.Business;
using JobManager.JobManagerService.Ioc;
using Quartz;

namespace JobManager.JobManagerService.Quartz
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
