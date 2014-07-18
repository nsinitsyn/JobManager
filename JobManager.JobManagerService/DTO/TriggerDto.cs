using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.JobManagerService.DTO
{
    [DataContract]
    public class TriggerDto
    {
        [DataMember]
        public string Cron { get; set; }
    }
}
