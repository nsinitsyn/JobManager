using System;
using JobManager.Data.DTO;

namespace JobManager.Data
{
    public class JobManagerEventArgs : EventArgs
    {
        public JobEventData EventData { get; set; }
    }
}
