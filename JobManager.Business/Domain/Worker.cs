using System;

namespace JobManager.Business.Domain
{
    public class Worker
    {
        public Guid Id { get; set; }
        public Job Job { get; set; }
        public JobWorkerBase Instance { get; set; }
        public bool Completed { get; set; }
    }
}
