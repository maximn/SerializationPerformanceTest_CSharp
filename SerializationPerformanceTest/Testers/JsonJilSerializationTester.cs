using Jil;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class JsonJilSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private StreamReader streamReader;

        public JsonJilSerializationTester(TTestObject testObject)
            : base(testObject)
        {
        }

        protected override void Init()
        {
            base.Init();
            streamReader = new StreamReader(this.MemoryStream);
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Position = 0;
            return JSON.Deserialize<TTestObject>(streamReader);
        }

        protected override System.IO.MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            JSON.Serialize<TTestObject>(this.TestObject, streamWriter);
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
