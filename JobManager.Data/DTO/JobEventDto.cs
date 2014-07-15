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
        public JobEventDto()
        {
            IsReturnResult = false;
        }

        [DataMember]
        public WorkerDto Worker { get; set; }

        [DataMember]
        public TransferData TransferData { get; set; }

        [DataMember]
        public bool IsReturnResult { get; set; }
    }
}
