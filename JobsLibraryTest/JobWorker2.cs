using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobManager.Data.Business;
using JobManager.Data.DTO;
using JobManager.Data.Utilities;
using JobsLibraryTest.Parameters;
using Tests.Utilities;

namespace JobsLibraryTest
{
    class JobWorker2 : JobWorkerBase
    {
        protected override object Run(object data)
        {
            Logger.Log.Info("Worker 2 start running");

            //var sJobData = parameters.SerializedJobData;

            //var p = (JobWorkerParameters)Serializator.DeserializeFromMemory(sJobData);

            var p = (JobWorkerParameters) data;

            //Thread.Sleep(10000);

            return new TransferData(new JobWorkerOutput { Result = "Worker 2 - It's OK!" });
        }

        protected override object Signal(object data)
        {
            // ...
            return null;
        }
    }
}
