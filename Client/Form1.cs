using System;
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
using JobManager.Data.DTO;
using JobManager.Data.Utilities;
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

            //var jobInput = new JobInputData
            //{
            //    JobWorkerClassName = jobName,
            //    Data = new TransferData(jobData)
            //};
            //var jobInput2 = new JobInputData
            //{
            //    JobWorkerClassName = "JobsLibraryTest.JobWorker2",
            //    Data = new TransferData(jobData)
            //};

            var context = new InstanceContext(new JobManagerServiceCallback());
            var client = new JobManagerServiceClient(context);
            //var task = new Task<TransferData>(() => client.RunJob(jobInput));
            //task.Start();
            //task.ContinueWith(t =>
            //{
            //    // WCF: ответ не приходит, пока не выполнится callback. Но при этом на стороне wcf он выполняется как асинхронный
            //    Logger.Log.Info("Пришел ответ от Worker 1");
            //    var res = t.Result;
            //    var r = (JobWorkerOutput)res.GetData();
            //    var xx = "";
            //});


            var task = new Task<TransferData>(() => client.RunJob(new JobDto { ClassName = jobName, Data = new TransferData(jobData) }));
            task.Start();
            task.ContinueWith(t =>
            {
                // WCF: ответ не приходит, пока не выполнится callback. Но при этом на стороне wcf он выполняется как асинхронный
                Logger.Log.Info("Пришел ответ от Worker 1");
                var res = t.Result;
                var r = res.GetData() as JobWorkerOutput;
                var xx = "";
            });

            Thread.Sleep(2000);
            client.Signal(new WorkerDto(), new TransferData("stop"));
        }
    }
}
