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
        TransferData Run(object data);
        TransferData Signal(object data);
    }
}
