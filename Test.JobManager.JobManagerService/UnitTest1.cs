using System;
using JobManager.Business;
using JobManager.Business.Domain;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.JobManager.JobManagerService
{
    [TestFixture]
    public class CommonTest
    {
        [Test]
        public void Case1()
        {
            // Запустить джобу
            // Убедиться, что после ее завершения и после исключения она в статусе Completed
            // Убедиться, что после ее завершения ей нельзя слать сигналы

            var job = new Job
                          {
                              ClassName = "Test.JobManager.JobsLibrary.JobWorker1"
                          };

            var jobRepository = MockRepository.GenerateStub<IJobRepository>();
            var unitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            var jobManagerSettings = MockRepository.GenerateStub<IUnitOfWork>();

            var runner = new JobRunner(jobRepository, unitOfWork);

            var worker = runner.RunJob(job);
        }
    }
}
