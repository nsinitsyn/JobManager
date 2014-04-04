using System;
using JobManager.Data.DTO;

namespace JobManager.Data
{
    public class JobManagerEventArgs : EventArgs
    {
        public JobEventDataBase EventData { get; set; }
    }
}
