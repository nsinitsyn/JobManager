using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.DTO
{
    [DataContract]
    public class JobDto
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public TransferData Data { get; set; }

        [DataMember]
        public List<TriggerDto> Triggers { get; set; }
    }
}
