using System;
using System.Runtime.Serialization;

namespace JobManager.Transfer.Data.DTO
{
    [DataContract]
    public class JobEventDto
    {
        public JobEventDto()
        {
            IsReturnResult = false;
        }

        [DataMember]
        public Guid WorkerId { get; set; }

        [DataMember]
        public TransferData TransferData { get; set; }

        [DataMember]
        public bool IsReturnResult { get; set; }
    }
}
