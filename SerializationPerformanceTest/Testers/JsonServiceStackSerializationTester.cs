using System.Diagnostics;
using System.IO;
using ServiceStack.Text;

namespace SerializationPerformanceTest.Testers
{
    class JsonServiceStackSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly TypeSerializer<TTestObject> serializer;
        private StreamReader streamReader;

        public JsonServiceStackSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            serializer = new TypeSerializer<TTestObject>();
        }

        protected override void Init()
        {
            base.Init();

            streamReader = new StreamReader(this.MemoryStream);
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return serializer.DeserializeFromReader(streamReader);
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            sw.Start();

            serializer.SerializeToWriter(base.TestObject, streamWriter);
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
