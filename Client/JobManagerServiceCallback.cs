using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.JobManagerReference;
using JobManager.Transfer.Data.DTO;
using JobsLibraryTest;

namespace Client
{
    public class JobManagerServiceCallback : IJobManagerServiceCallback
    {
        public void OnEvent(JobEventDto eventDto)
        {
            if (eventDto.WorkerId == WorkersKeeper.Worker1) // eventDto.Worker.Id == "workerConcreteId"
            {
                if (eventDto.IsReturnResult)
                {
                    var returnResult = eventDto.TransferData.GetData() as JobWorkerOutput;
                    //Thread.Sleep(8000);
                }
                else
                {
                    var data = (string)eventDto.TransferData.GetData();
                    //Thread.Sleep(7000);
                }
            }

            //if (eventData.WorkerType == typeof(JobWorker).ToString())
            //{
                //var data = (string)eventData.TransferData.GetData();

                //var context = new InstanceContext(new JobManagerServiceCallback());
                //var client = new JobManagerServiceClient(context);
                //client.SendSignal(eventData.WorkerId, new TransferData("signalData"));
            //}

            //Thread.Sleep(10000);
        }

        public TransferData OnEventSync(JobEventDto eventDto)
        {
            // returnResult приходит только асинхронно

            if (eventDto.WorkerId == WorkersKeeper.Worker1)
            {
                var data = (string)eventDto.TransferData.GetData();
                //Thread.Sleep(7000);
                return new TransferData("EventSync result");
            }
            return null;
        }

        public void WorkerWillBeStarted(WorkerDto worker)
        {
            // Если эта джоба была запущена не нами, то подписаться на ее эвенты
            // ...

            Thread.Sleep(6000);

            var context = new InstanceContext(new JobManagerServiceCallback());
            var client = new JobManagerServiceClient(context);
            client.SetClientContextToWorker(worker.Id);
        }

        public Guid ClientIdentifier()
        {
            var clientId = ConfigurationManager.AppSettings["JobManagerClientIdentifier"];
            var clientGuid = Guid.Parse(clientId);
            return clientGuid;
        }
    }
}
