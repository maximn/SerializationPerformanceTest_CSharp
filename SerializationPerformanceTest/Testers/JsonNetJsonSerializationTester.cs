using System.Diagnostics;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class JsonNetJsonSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        public JsonNetJsonSerializationTester(TTestObject testObject)
            : base(testObject) { }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            sw.Stop();
            MemoryStream.Seek(0, 0);
            var reader = new StreamReader(MemoryStream);
            sw.Start();

            return NetJSON.NetJSON.Deserialize<TTestObject>(reader);
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var writer = new StreamWriter(new MemoryStream());
            sw.Start();

            NetJSON.NetJSON.Serialize(TestObject, writer);
            writer.Flush();
            return (MemoryStream)writer.BaseStream;
        }
    }
}
