using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.JobManagerReference;
using JobManager.Data.DTO;
using JobManager.Data.Utilities;
using JobsLibraryTest;

using JobEventData = Client.JobManagerReference.JobEventData;

namespace Client
{
    public class JobManagerServiceCallback : IJobManagerServiceCallback
    {
        public void OnEvent(JobEventData eventData)
        {
            // Общий для всех задач. Нужно определить тип задачи и тип события
            // ...

            if (eventData.WorkerType == typeof(JobWorker).ToString())
            {
                var data = (string)eventData.TransferData.GetData();

                //var context = new InstanceContext(new JobManagerServiceCallback());
                //var client = new JobManagerServiceClient(context);
                //client.SendSignal(eventData.WorkerId, new TransferData("signalData"));
            }

            //Thread.Sleep(10000);
        }
    }
}
