using System.Diagnostics;
using System.IO;
using ZeroFormatter;

namespace SerializationPerformanceTest.Testers
{
    public class ZeroFormatterTester<TTestObject> : SerializationTester<TTestObject>
    {
        public ZeroFormatterTester(TTestObject testObject)
            : base(testObject) { }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return ZeroFormatterSerializer.Deserialize<TTestObject>(MemoryStream);
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            ZeroFormatterSerializer.Serialize(stream, TestObject);
            return stream;
        }
    }
}
