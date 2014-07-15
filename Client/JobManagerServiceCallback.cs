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

namespace Client
{
    public class JobManagerServiceCallback : IJobManagerServiceCallback
    {
        public void OnEvent(JobEventDto eventDto)
        {
            // Общий для всех задач. Нужно определить тип задачи и тип события
            // ...

            if (eventDto.Worker != null) // eventDto.Worker.Id == "workerConcreteId"
            {
                if (eventDto.IsReturnResult)
                {
                    var returnResult = eventDto.TransferData.GetData() as JobWorkerOutput;                    
                }
                else
                {
                    var data = (string)eventDto.TransferData.GetData();                                        
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
    }
}
