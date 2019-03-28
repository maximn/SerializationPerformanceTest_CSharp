using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class DataContractSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly DataContractSerializer serializer;

        public DataContractSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            serializer = new DataContractSerializer(typeof(TTestObject));
        }

        protected override TTestObject Deserialize(Stopwatch sw)
        {
            MemoryStream.Seek(0, 0);
            return (TTestObject)serializer.ReadObject(MemoryStream);
        }
        
        protected override MemoryStream Serialize(Stopwatch sw)
        {
            sw.Stop();
            var stream = new MemoryStream();
            sw.Start();

            serializer.WriteObject(stream, TestObject);
            return stream;
        }
    }
}
