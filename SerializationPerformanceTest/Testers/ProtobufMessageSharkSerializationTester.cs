using MessageShark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    class ProtobufMessageSharkSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        public ProtobufMessageSharkSerializationTester(TTestObject testObject)
            : base(testObject)
        {
        }

        protected override TTestObject Deserialize()
        {
            base.MemoryStream.Position = 0;
            return MessageSharkSerializer.Deserialize<TTestObject>(this.MemoryStream.ToArray());
        }

        protected override System.IO.MemoryStream Serialize()
        {
            var buffer = MessageSharkSerializer.Serialize(this.TestObject);
            return new MemoryStream(buffer);
        }
    }
}
