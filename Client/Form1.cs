﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.JobManagerReference;
using JobManager.Transfer.Data.DTO;
using JobsLibraryTest;
using JobsLibraryTest.Parameters;
using Tests.Utilities;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string jobName = "JobsLibraryTest.JobWorker";
            var jobData = new JobWorkerParameters
                            {
                                FileName = "testJMService.txt"
                            };

            var context = new InstanceContext(new JobManagerServiceCallback());
            var client = new JobManagerServiceClient(context);

            var jobDto = new JobDto
                             {
                                 ClassName = jobName,
                                 Data = new TransferData(jobData)
                             };

            var t = new Task(() => client.SubscribeClientContext());
            t.ContinueWith(s => { var workerDto = client.RunJob(jobDto); });
            t.Start();

            //var workerDto = client.RunJob(jobDto);
            //WorkersKeeper.Worker1 = workerDto.Id;
            //Thread.Sleep(2000);
            //client.Signal(workerDto.Id, new TransferData("stop"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string jobName = "JobsLibraryTest.JobWorker";
            var jobData = new JobWorkerParameters
            {
                FileName = "testJMService.txt"
            };

            var context = new InstanceContext(new JobManagerServiceCallback());
            var client = new JobManagerServiceClient(context);

            var jobDto = new JobDto
            {
                ClassName = jobName,
                Data = new TransferData(jobData),
                Triggers = new List<TriggerDto>
                               {
                                   new TriggerDto
                                       {
                                           Cron = "0 0/1 * * * ?"
                                       }
                               }
            };

            var jobId = client.ScheduleJob(jobDto);
        }
    }
}
