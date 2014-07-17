using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork()
        {
            _dataContext = DataContext.DefaultContext;
        }

        public void Commit()
        {
            _dataContext.SaveChanges();
        }
    }
}
