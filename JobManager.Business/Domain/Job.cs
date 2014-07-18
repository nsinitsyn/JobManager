using System;
using System.Collections.Generic;

namespace JobManager.Business.Domain
{
    public class Job
    {
        public Job()
        {
            Workers = new List<Worker>();
            Triggers = new List<Trigger>();
        }

        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public object Data { get; set; }
        public List<Worker> Workers { get; set; }
        public List<Trigger> Triggers { get; set; }

        public bool Scheduled
        {
            get { return JobRunner.Runner.HasJobScheduled(Id); }
        }
    }
}
