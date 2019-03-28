using System.Diagnostics;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class ProtobufSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        public ProtobufSerializationTester(TTestObject testObject)
            : base(testObject) { }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return ProtoBuf.Serializer.Deserialize<TTestObject>(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            ProtoBuf.Serializer.Serialize(stream, TestObject);
            return stream;
        }
    }
}
