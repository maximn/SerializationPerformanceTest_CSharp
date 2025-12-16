using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SerializationPerformanceTest.Testers
{
    class JsonNewtonsoftSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        public JsonNewtonsoftSerializationTester(TTestObject testObject)
            : base(testObject)
        {
        }


        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Position = 0;
            return JsonConvert.DeserializeObject<TTestObject>(FromBytes(base.MemoryStream.ToArray()));
        }

        protected override MemoryStream Serialize()
        {
            return new MemoryStream(ToBytes(JsonConvert.SerializeObject(base.TestObject)));
        }
        public static byte[] ToBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string FromBytes(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

    }
}