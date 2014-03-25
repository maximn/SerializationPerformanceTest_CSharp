using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SerializationPerformanceTest.Testers
{
    class JsonNewtonsoftSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private string text;
        private JsonSerializer jsonSerializer;


        public JsonNewtonsoftSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            text = File.ReadAllText(this.SourceDataFilename);
            jsonSerializer = new JsonSerializer();
        }

        protected override TTestObject Deserialize()
        {
            return JsonConvert.DeserializeObject<TTestObject>(text);
        }

        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            jsonSerializer.Serialize(streamWriter, obj);
            streamWriter.Flush();

            return stream;
        }
    }
}