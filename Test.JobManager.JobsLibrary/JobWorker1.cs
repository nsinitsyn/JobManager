using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Business;

namespace Test.JobManager.JobsLibrary
{
    public class JobWorker1 : JobWorkerBase
    {
        protected override object Run(object data)
        {
            throw new NotImplementedException();
        }

        protected override object Signal(object data)
        {
            throw new NotImplementedException();
        }
    }
}
