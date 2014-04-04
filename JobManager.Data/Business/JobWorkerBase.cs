using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;

namespace JobManager.Data.Business
{
    public abstract class JobWorkerBase : IJobWorker
    {
        public event JobManagerEventHandler OnEvent;

        public abstract JobOutputDataBase Run(JobInputDataBase parameters);

        public void SendEvent(JobEventDataBase eventData)
        {
            OnEvent(this, new JobManagerEventArgs { EventData = eventData });
        }
    }
}
