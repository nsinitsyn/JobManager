using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Utilities;

namespace JobManager.Data.DTO
{
    [DataContract]
    public class TransferData
    {
        [DataMember]
        public MemoryStream SerializedData { get; set; }

        public TransferData(object data)
        {
            SerializedData = Serializator.SerializeToMemory(data);
        }

        public object GetData()
        {
            return Serializator.DeserializeFromMemory(SerializedData);
        }
    }
}
