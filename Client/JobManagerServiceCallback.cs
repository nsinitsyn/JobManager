using System;
using System.Collections.Generic;
using System.Linq;
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
        public void OnEvent(JobEventDataBase eventData)
        {
            // Общий для всех задач. Нужно определить тип задачи и тип события
            // ...

            if (eventData.WorkerType == typeof(JobWorker).ToString())
            {
                var data = (string)Serializator.DeserializeFromMemory(eventData.SerializedData);
            }

            Thread.Sleep(10000);
        }

        //public void OnEvent(int eventData)
        //{
        //    var t = eventData;
        //}
    }
}
