using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
