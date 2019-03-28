using MsgPack.Serialization;
using System.Diagnostics;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly MessagePackSerializer<TTestObject> serializer;

        public MsgPackSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            serializer = MessagePackSerializer.Get<TTestObject>();            
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return serializer.Unpack(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            serializer.Pack(stream, TestObject);
            return stream;
        }
    }
}
