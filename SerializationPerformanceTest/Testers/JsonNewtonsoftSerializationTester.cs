using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SerializationPerformanceTest.Testers
{
    class JsonNewtonsoftSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly JsonSerializer jsonSerializer;
        private StreamReader streamReader;


        public JsonNewtonsoftSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            jsonSerializer = new JsonSerializer();
        }

        protected override void Init()
        {
            base.Init();
            streamReader = new StreamReader(this.MemoryStream);
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            sw.Stop();
            MemoryStream.Seek(0, 0);
            var jsonTextReader = new JsonTextReader(streamReader) { CloseInput = false };
            sw.Start();

            return jsonSerializer.Deserialize<TTestObject>(jsonTextReader);
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            sw.Start();

            jsonSerializer.Serialize(streamWriter, TestObject);
            streamWriter.Flush();
            return stream;
        }

        public override void Dispose()
        {
            streamReader.Dispose();
            base.Dispose();
        }
    }
}
