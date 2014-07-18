using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JobManager.Data.Utilities
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

            //XmlSerializer xmlSerializer = null;
            //var type = objectGraph.GetType();
            //try
            //{
            //    xmlSerializer = new XmlSerializer(type);
            //}
            //catch
            //{
            //}
            //var writer = new StringWriter();

            //xmlSerializer.Serialize(writer, objectGraph);
            //var dataString = writer.ToString();
            //return dataString;
        }

        public static object DeserializeFromString(string dataString)
        {
            var bytes = Convert.FromBase64String(dataString);
            var result = DeserializeFromMemory(new MemoryStream(bytes));
            return result;

            //var strReader = new StringReader(dataString);
            //var xmlSerializer = new XmlSerializer(typeof(object));
            //var xmlReader = new XmlTextReader(strReader);
            //try
            //{
            //    var data = xmlSerializer.Deserialize(xmlReader);
            //    return data;
            //}
            //finally
            //{
            //    xmlReader.Close();
            //    strReader.Close();
            //}
        }
    }
}
