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
    public class JobInputData
    {
        [DataMember]
        public string JobWorkerClassName { get; set; }

        [DataMember]
        public TransferData Data { get; set; }
    }
}
