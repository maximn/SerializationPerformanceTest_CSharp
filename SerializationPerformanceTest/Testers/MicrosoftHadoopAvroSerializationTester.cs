using System.IO;
using Microsoft.Hadoop.Avro;

namespace SerializationPerformanceTest.Testers
{
    class MicrosoftHadoopAvroSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private readonly IAvroSerializer<TTestObject> _avroSerializer;
        public MicrosoftHadoopAvroSerializationTester(TTestObject testObject)
            : base(testObject)
        {
            _avroSerializer = AvroSerializer.Create<TTestObject>();
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Seek(0, 0);
            return _avroSerializer.Deserialize(base.MemoryStream);
        }
        
        protected override MemoryStream Serialize()
        {
            var buffer = new MemoryStream();
            _avroSerializer.Serialize(buffer, base.TestObject);
            return buffer;
        }
    }
}
