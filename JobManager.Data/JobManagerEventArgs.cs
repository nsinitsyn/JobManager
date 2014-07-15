using System;
using JobManager.Data.DTO;

namespace JobManager.Data
{
    public class JobManagerEventArgs : EventArgs
    {
        public JobEventDto EventDto { get; set; }
    }
}
