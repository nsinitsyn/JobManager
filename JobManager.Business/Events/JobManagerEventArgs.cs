using System;
using JobManager.Business.Domain;

namespace JobManager.Business.Events
{
    public class JobManagerEventArgs : EventArgs
    {
        public JobEvent Event { get; set; }
    }
}
