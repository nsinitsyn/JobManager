using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JobManager.Business.Utilities
{
    public static class Serializator
    {
        public static MemoryStream SerializeToMemory(object objectGraph)
        {
            var stream = new MemoryStream();
            new BinaryFormatter().Serialize(stream, objectGraph);
            stream.Position = 0;
            return stream;
        }

        public static object DeserializeFromMemory(Stream stream)
        {
            var result = new BinaryFormatter().Deserialize(stream);
            return result;
        }

        public static string SerializeToString(object objectGraph)
        {
            var stream = SerializeToMemory(objectGraph);
            var result = Convert.ToBase64String(stream.ToArray());
            stream.Close();
            return result;
        }

        public static object DeserializeFromString(string dataString)
        {
            var bytes = Convert.FromBase64String(dataString);
            var result = DeserializeFromMemory(new MemoryStream(bytes));
            return result;
        }
    }
}
