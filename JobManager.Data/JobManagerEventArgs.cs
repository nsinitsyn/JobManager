using System;
using JobManager.Data.Domain;

namespace JobManager.Data
{
    public class JobManagerEventArgs : EventArgs
    {
        public JobEvent Event { get; set; }
    }
}
