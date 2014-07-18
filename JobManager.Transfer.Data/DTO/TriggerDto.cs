
using System.Runtime.Serialization;

namespace JobManager.Transfer.Data.DTO
{
    [DataContract]
    public class TriggerDto
    {
        [DataMember]
        public string Cron { get; set; }
    }
}
