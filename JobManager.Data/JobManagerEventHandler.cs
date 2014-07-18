using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data
{
    public delegate void JobManagerEventHandler(object sender, JobManagerEventArgs e);
    public delegate object JobManagerEventSyncHandler(object sender, JobManagerEventArgs e);
}
