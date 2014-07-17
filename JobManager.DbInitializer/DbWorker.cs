using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;
using JobManager.Data.Database.UnitOfWork;

namespace JobManager.DbInitializer
{
    internal class DbWorker
    {
        private readonly IJobRepository _jobRepository;

        public DbWorker(
            IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;

            DbManager.SetAutoMigrateInitializer();
        }

        public bool DeleteAllDataFromDb()
        {
            var dbManager = new DbManager();
            return dbManager.RecreateDatabase();
        }

        public void FillDbWithTestData()
        {
            
        }
    }
}
