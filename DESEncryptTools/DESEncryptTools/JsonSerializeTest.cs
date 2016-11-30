using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DESEncryptTools
{
    public class JsonSerializeTest<T>
    {
        public static string SaveToJson(T obj)
        {
            using (MemoryStream memstream = new MemoryStream())
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                deseralizer.WriteObject(memstream, obj);
                StreamReader reader = new StreamReader(memstream);
                memstream.Position = 0;
                string json = reader.ReadToEnd();
                reader.Dispose();

                return json;
            }
        }

        public static T ReadFromJson(string json)
        {
            using (MemoryStream memstream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(memstream);
                writer.Write(json);
                writer.Flush();
                memstream.Position = 0;
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                T obj = (T)deseralizer.ReadObject(memstream);// //反序列化ReadObject
                writer.Dispose();

                return obj;
            }
        }
    }

    [DataContract]
    public class TestModel
    {
        [DataMember]
        public bool GatewayEnable = false;

        [DataMember]
        public string Gateway = "";

        [DataMember]
        public bool PrimaryDNSEnable = false;

        [DataMember]
        public string PrimaryDNS = "";

        [DataMember]
        public bool SecondaryDNSEnable = false;

        //[DataMember]
        //public string SecondaryDNS = "";

        [DataMember]
        public string name = "";

        [DataMember]
        public int id = 1;

        public TestModel()
        {
            id = 66;
            name = "ttabc123";
        }
    }
}
