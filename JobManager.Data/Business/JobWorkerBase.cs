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

        public abstract TransferData Run(JobInputData parameters);

        public void SendEvent(JobEventData eventData)
        {
            OnEvent(this, new JobManagerEventArgs { EventData = eventData });
        }
    }
}
