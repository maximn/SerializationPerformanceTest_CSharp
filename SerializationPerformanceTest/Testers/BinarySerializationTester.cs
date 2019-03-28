using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationPerformanceTest.Testers
{
    class BinarySerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly IFormatter formatter;

        public BinarySerializationTester(TTestObject testObject)
            : base(testObject)
        {
            formatter = new BinaryFormatter();
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return (TTestObject)formatter.Deserialize(MemoryStream);
        }


        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            formatter.Serialize(stream, TestObject);
            return stream;
        }
    }
}
