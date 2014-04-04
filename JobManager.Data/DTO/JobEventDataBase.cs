using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.DTO
{
    [DataContract]
    [Serializable]
    public class JobEventDataBase
    {
        [DataMember]
        public string WorkerType { get; set; }

        [DataMember]
        public MemoryStream SerializedData { get; set; }
    }
}
