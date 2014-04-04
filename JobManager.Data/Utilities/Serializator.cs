using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Utilities
{
    public static class Serializator
    {
        public static MemoryStream SerializeToMemory(Object objectGraph)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, objectGraph);
            stream.Position = 0;
            return stream;
        }

        public static object DeserializeFromMemory(Stream stream)
        {
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
