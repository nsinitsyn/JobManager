using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;

namespace JobManager.Data.Business
{
    public abstract class JobWorkerBase
    {
        public event JobManagerEventHandler OnEvent;

        protected abstract TransferData Run(object data);
        protected abstract TransferData Signal(object data);

        public TransferData RunWrap(object data)
        {
            var result = Run(data);
            return result;
        }

        public TransferData SignalWrap(object data)
        {
            var result = Signal(data);
            return result;
        }

        public void SendEvent(JobEventDto eventDto)
        {
            OnEvent(this, new JobManagerEventArgs { EventDto = eventDto });
        }
    }
}
