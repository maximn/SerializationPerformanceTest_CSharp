using Jil;
using System.Diagnostics;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class JsonJilSerializationTesterTester<TTestObject> : SerializationTester<TTestObject>
    {
        public JsonJilSerializationTesterTester(TTestObject testObject)
            : base(testObject) { }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            sw.Stop();
            MemoryStream.Seek(0, 0);
            var reader = new StreamReader(MemoryStream);
            sw.Start();

            return JSON.Deserialize<TTestObject>(reader);
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var writer = new StreamWriter(new MemoryStream());
            sw.Start();

            JSON.Serialize(TestObject, writer);
            writer.Flush();
            return (MemoryStream)writer.BaseStream;
        }
    }
}
