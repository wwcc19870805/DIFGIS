using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace DFCommon.Class
{
    public static class JsonTool
    {
        // Methods
        public static T JsonToObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T local = (T)serializer.ReadObject(stream);
                stream.Close();
                return local;
            }
            catch (Exception exception)
            {
                return default(T);
            }
        }

        public static string ObjectToJson(object item)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
                MemoryStream stream = new MemoryStream();
                serializer.WriteObject(stream, item);
                StringBuilder builder = new StringBuilder();
                builder.Append(Encoding.UTF8.GetString(stream.ToArray()));
                stream.Close();
                return builder.ToString();
            }
            catch (Exception exception)
            {
                return string.Empty;
            }
        }
    }
}
