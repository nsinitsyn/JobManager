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

            var jobInput = new JobInputDataBase
            {
                JobWorkerClassName = jobName,
                SerializedJobData = Serializator.SerializeToMemory(jobData)
            };
            var jobInput2 = new JobInputDataBase
            {
                JobWorkerClassName = "JobsLibraryTest.JobWorker2",
                SerializedJobData = Serializator.SerializeToMemory(jobData)
            };

            //var service = new ChannelFactory<IJobManagerService>("WsHttpJobManager").CreateChannel();

            //var task = new Task<JobOutputDataBase>(() => service.RunJob(jobInput));
            //Logger.Log.Info("Запуск Worker 1");
            //task.Start();

            //task.ContinueWith(t =>
            //                      {
            //                          Logger.Log.Info("Пришел ответ от Worker 1");
            //                          var res = t.Result;
            //                          var r = (JobWorkerOutput)Serializator.DeserializeFromMemory(res.SerializedResult);
            //                          var xx = "";
            //                      });

            //var task2 = new Task<JobOutputDataBase>(() => service.RunJob(jobInput2));
            //Logger.Log.Info("Запуск Worker 2");
            //task2.Start();

            //task2.ContinueWith(t =>
            //{
            //    Logger.Log.Info("Пришел ответ от Worker 2");
            //    var res = t.Result;
            //    var r = (JobWorkerOutput)Serializator.DeserializeFromMemory(res.SerializedResult);
            //    var xx = "";
            //});



            //var context = new InstanceContext(new JobManagerServiceCallback());

            //using (var client = new JobManagerServiceClient.JobManagerServiceClient(context))
            //{
            //    var x = client.RunJob(jobInput);
            //}

            var context = new InstanceContext(new JobManagerServiceCallback());
            var client = new JobManagerServiceClient(context);
            var task = new Task<JobOutputDataBase>(() => client.RunJob(jobInput));
            task.Start();
            task.ContinueWith(t =>
            {
                // WCF: ответ не приходит, пока не выполнится callback. Но при этом на стороне wcf он выполняется как асинхронный
                Logger.Log.Info("Пришел ответ от Worker 1");
                var res = t.Result;
                var r = (JobWorkerOutput)Serializator.DeserializeFromMemory(res.SerializedResult);
                var xx = "";
            });

            //var tt = client.RunJobAsync(jobInput);
            //tt.ContinueWith(t =>
            //{
            //    Logger.Log.Info("Пришел ответ от Worker 1");
            //    var res = t.Result;
            //    var r = (JobWorkerOutput)Serializator.DeserializeFromMemory(res.SerializedResult);
            //    var xx = "";
            //});

            //var tt2 = client.RunJobAsync(jobInput2);
            //tt2.ContinueWith(t =>
            //{
            //    Logger.Log.Info("Пришел ответ от Worker 2");
            //    var res = t.Result;
            //    var r = (JobWorkerOutput)Serializator.DeserializeFromMemory(res.SerializedResult);
            //    var xx = "";
            //});
        }
    }
}
