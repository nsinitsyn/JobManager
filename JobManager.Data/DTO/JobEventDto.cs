using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.DTO
{
    [DataContract]
    public class JobEventDto
    {
        [DataMember]
        public WorkerDto Worker { get; set; }

        [DataMember]
        public TransferData TransferData { get; set; }
    }
}
