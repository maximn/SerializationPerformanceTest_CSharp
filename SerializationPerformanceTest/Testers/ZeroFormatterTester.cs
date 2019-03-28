using System.IO;
using ZeroFormatter;

namespace SerializationPerformanceTest.Testers
{
    public class ZeroFormatterTester<TTestObject> : SerializationTester<TTestObject>
    {
        public ZeroFormatterTester(TTestObject testObject)
            : base(testObject) { }

        protected override TTestObject Deserialize() => ZeroFormatterSerializer.Deserialize<TTestObject>(MemoryStream);

        protected override MemoryStream Serialize()
        {
            var stream = new MemoryStream();
            ZeroFormatterSerializer.Serialize(stream, TestObject);
            return stream;
        }
    }
}
