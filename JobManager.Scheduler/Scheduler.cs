using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace JobManager.Scheduler
{
    public class Scheduler
    {
        private const string GroupJobs = "JobManagerGroup";
        private IScheduler _scheduler;

        public void Start()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
            _scheduler.Start();
        }

        public void Stop()
        {
            if (_scheduler != null)
            {
                _scheduler.Shutdown();
                _scheduler = null;
            }
        }

        public void ScheduleJob(Guid jobId, string classPath, string firstStart, List<string> triggerStrings)
        {
            var classType = Type.GetType(classPath);

            var job = JobBuilder.Create(classType)
                .WithIdentity(JobKeyForJobId(jobId))
                .Build();

            foreach(var triggerString in triggerStrings)
            {
                var trigger = TriggerBuilder.Create()
                    .ForJob(job)
                    .Build();

                _scheduler.ScheduleJob(trigger);
            }
        }

        public void DeleteJob(Guid jobId)
        {
            var jobKey = JobKeyForJobId(jobId);
            if (_scheduler.CheckExists(jobKey))
            {
                _scheduler.DeleteJob(jobKey);
            }
        }

        private JobKey JobKeyForJobId(Guid jobId)
        {
            return new JobKey(jobId.ToString(), GroupJobs);
        }
    }
}
