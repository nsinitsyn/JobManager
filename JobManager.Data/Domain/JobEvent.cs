using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Domain
{
    public class JobEvent
    {
        public object Data { get; set; }
        public Guid WorkerId { get; set; }
        public bool IsReturnResult { get; set; }
    }
}
