﻿using System;
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
        private bool Stop = false;

        protected override TransferData Run(object data)
        {
            //Logger.Log.Info("Worker 1 start running");

            //var p = (JobWorkerParameters) data;

            //var eventDto = new JobEventDto
            //                    {
            //                        TransferData = new TransferData("eventData from Worker1")
            //                    };

            //SendEvent(eventDto);

            //Thread.Sleep(10000);

            //return new TransferData(new JobWorkerOutput {Result = "Worker 1 - It's OK!"});

            while (true)
            {
                if (Stop)
                {
                    break;
                }

                Thread.Sleep(5000);
            }

            return null;
        }

        protected override TransferData Signal(object data)
        {
            var d = (string) data;
            if (d == "stop")
            {
                Stop = true;
            }
            return null;
        }

        //public JobOutputDataBase SendSignal(SignalToken signalToken)
        //{
            
        //}
    }
}
