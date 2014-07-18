using System.IO;
using System.Runtime.Serialization;
using JobManager.Business.Utilities;

namespace JobManager.Transfer.Data.DTO
{
    [DataContract]
    public class TransferData
    {
        [DataMember]
        public MemoryStream SerializedData { get; set; }

        public TransferData(object data)
        {
            SerializedData = data != null ? Serializator.SerializeToMemory(data) : null;
        }

        public object GetData()
        {
            return SerializedData != null ? Serializator.DeserializeFromMemory(SerializedData) : null;
        }
    }
}
