using System.IO;
using System.Xml.Serialization;

namespace MongoSharp.Model
{
    public class Serializer
    {
        public void SerializeObject<T>(string filename, T objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            var bFormatter = new XmlSerializer(typeof(T));
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public T DeSerializeObject<T>(string filename)
        {
            Stream stream = File.Open(filename, FileMode.Open);
            var bFormatter = new XmlSerializer(typeof(T));
            T objectToSerialize = (T)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
