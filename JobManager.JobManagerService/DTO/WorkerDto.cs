using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.JobManagerService.DTO
{
    public class WorkerDto
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
    }
}
