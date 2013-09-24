using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace ExtensionJson
{
        //Links to examples on DataContractJsonSerialization:
        // http://kashfarooq.wordpress.com/2011/01/31/creating-net-objects-from-json-using-datacontractjsonserializer/
        // http://www.codeproject.com/Articles/272335/JSON-Serialization-and-Deserialization-in-ASP-NET
        public static class JsonHelper
        {
            public static string ToJson(this object obj)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(obj);
            }
            public static string ToJson(this object obj, int recursionDepth)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RecursionLimit = recursionDepth;
                return serializer.Serialize(obj);
            }

            public static T JsonDeserializer<T>(string _json)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(_json));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
        }
}
