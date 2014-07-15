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

        private WorkerDto _workerDto;

        public TransferData RunWrap(object data, WorkerDto workerDto)
        {
            _workerDto = workerDto;

            var result = Run(data);
            return result;
        }

        public TransferData SignalWrap(object data)
        {
            var result = Signal(data);
            return result;
        }

        public void SendEvent(object eventData)
        {
            OnEvent(this, new JobManagerEventArgs
                              {
                                  EventDto = new JobEventDto
                                                 {
                                                     TransferData = new TransferData(eventData),
                                                     Worker = _workerDto
                                                 }
                              });
        }
    }
}
