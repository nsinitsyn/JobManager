using JobManager.Business.Domain;
using JobManager.Business.Events;

namespace JobManager.Business
{
    public abstract class JobWorkerBase
    {
        public event JobManagerEventHandler OnEvent;
        public event JobManagerEventSyncHandler OnEventSync;

        protected abstract object Run(object data);
        protected abstract object Signal(object data);

        private Worker _worker;

        public object RunWrap(object data, Worker worker)
        {
            _worker = worker;

            var result = Run(data);
            return result;
        }

        public object SignalWrap(object data)
        {
            var result = Signal(data);
            return result;
        }

        public void SendEvent(object eventData)
        {
            OnEvent(this, new JobManagerEventArgs
                              {
                                  Event = new JobEvent
                                              {
                                                  Data = eventData,
                                                  WorkerId = _worker.Id
                                              }
                              });
        }

        public object SendEventSync(object eventData)
        {
            var result = OnEventSync(this, new JobManagerEventArgs
                                               {
                                                   Event = new JobEvent
                                                               {
                                                                   Data = eventData,
                                                                   WorkerId = _worker.Id
                                                               }
                                               });

            return result;
        }
    }
}
