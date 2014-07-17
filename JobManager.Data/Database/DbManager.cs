using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Migrations;

namespace JobManager.Data.Database
{
    public class DbManager
    {
        private readonly DataContext _dataContext;

        public DbManager()
        {
            _dataContext = DataContext.DefaultContext;
        }

        public static void SetAutoMigrateInitializer()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }

        public bool RecreateDatabase()
        {
            var migrator = new DbMigrator(new Configuration());
            var migrations = migrator.GetLocalMigrations();
            if (_dataContext.Database.Exists())
            {
                //_dataContext.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction,
                //    string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                //    _dataContext.Database.Connection.Database));
                if (!_dataContext.Database.Delete())
                {
                    return false;
                }
            }

            if (migrations.Count() != 0)
            {
                migrator.Update();
            }
            else
            {
                _dataContext.Database.Create();
            }
            return true;
        }
    }
}
