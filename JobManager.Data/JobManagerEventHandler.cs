using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;

namespace JobManager.Data
{
    public delegate void JobManagerEventHandler(object sender, JobManagerEventArgs e);
}
