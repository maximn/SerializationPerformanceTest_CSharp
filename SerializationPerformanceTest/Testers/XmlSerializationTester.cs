using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class XmlSerializationTester<TTestObject> : SerializationTester<TTestObject>{
        private readonly XmlSerializer serializer;

        public XmlSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            serializer = new XmlSerializer(typeof(TTestObject));
            
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            base.MemoryStream.Seek(0, 0);
            var deserialize = (TTestObject)serializer.Deserialize(base.MemoryStream);
            return deserialize;
        }

        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            serializer.Serialize(stream, TestObject);
            return stream;
        }
    }
}
