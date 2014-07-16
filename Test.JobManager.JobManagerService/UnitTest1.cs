using System;
using NUnit.Framework;

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
        }
    }
}
