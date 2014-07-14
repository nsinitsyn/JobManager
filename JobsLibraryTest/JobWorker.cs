using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Utilities;
using JobsLibraryTest.Parameters;
using Tests.Utilities;
using log4net;
using log4net.Config;

namespace JobsLibraryTest
{
    public class JobWorker : JobWorkerBase
    {
        public override JobOutputDataBase Run(JobInputDataBase parameters)
        {
            Logger.Log.Info("Worker 1 start running");

            var sJobData = parameters.SerializedJobData;

            var p = (JobWorkerParameters)Serializator.DeserializeFromMemory(sJobData);

            // Кинуть синхронное событие с параметром и получить результат

            var eventData = new JobEventDataBase
                                {
                                    WorkerType = typeof(JobWorker).ToString(),
                                    SerializedData = Serializator.SerializeToMemory("eventData from Worker1")
                                };

            SendEvent(eventData);

            // Кинуть событие с параметром и получить результат
            //SendEventAsync(JobWorker1Event.WTF, eventDataBase, resAsync => { });

            //Thread.Sleep(10000);

            return new JobOutputDataBase
                       {
                           SerializedResult = Serializator.SerializeToMemory(new JobWorkerOutput { Result = "Worker 1 - It's OK!" })
                       };
        }

        public JobOutputDataBase SendSignal(SignalToken signalToken)
        {
            
        }
    }
}
