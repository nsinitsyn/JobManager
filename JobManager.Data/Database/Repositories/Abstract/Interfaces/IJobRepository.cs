using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;

namespace JobManager.Data.Database.Repositories.Abstract.Interfaces
{
    public interface IJobRepository : IBaseRepository<JobDb>
    {
    }
}
