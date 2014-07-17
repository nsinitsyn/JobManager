using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;

namespace JobManager.Data.Database.Repositories
{
    public class JobRepository : BaseRepository<JobDb>, IJobRepository
    {
    }
}
