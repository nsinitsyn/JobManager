using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;

namespace JobManager.Data.Business
{
    public interface IJobWorker
    {
        JobOutputDataBase Run(JobInputDataBase parameters);
    }
}
