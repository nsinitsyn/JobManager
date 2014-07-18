using System;

namespace JobManager.Business.Domain
{
    public class JobEvent
    {
        public object Data { get; set; }
        public Guid WorkerId { get; set; }
        public bool IsReturnResult { get; set; }
    }
}
