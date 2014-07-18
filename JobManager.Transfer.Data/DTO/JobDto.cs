using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JobManager.Transfer.Data.DTO
{
    [DataContract]
    public class JobDto
    {
        public JobDto()
        {
            Triggers = new List<TriggerDto>();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public TransferData Data { get; set; }

        [DataMember]
        public List<TriggerDto> Triggers { get; set; }

        [DataMember]
        public bool Scheduled { get; set; }
    }
}
