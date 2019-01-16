using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SUPMS.Infrastructure.Utilities
{
    public class JsonSerializer
    {
        /// <summary>
        /// Serialize from a JSON string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public string Serialize<T>(T jsonObject)
        {
            MemoryStream memStream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(jsonObject.GetType());
            serializer.WriteObject(memStream, jsonObject);

            memStream.Close();
            memStream.Dispose();

            string json = Encoding.Default.GetString(memStream.ToArray());

            return json;
        }

        /// <summary>
        /// Deserialize JSON in a stream
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public T Deserialize<T>(Stream stream)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T t = (T)serializer.ReadObject(stream);

            return t;
        }
    }
}
