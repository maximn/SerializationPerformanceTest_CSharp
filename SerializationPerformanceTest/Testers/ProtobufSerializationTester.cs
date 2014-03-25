using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SerializationPerformanceTest.Testers
{
    class ProtobufSerializationTester<TTestObject> : SerializationTester<TTestObject>, IDisposable
    {
        private MemoryStream memoryStream;


        public ProtobufSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(base.SourceDataFilename, FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
        }

        protected override TTestObject Deserialize()
        {
            memoryStream.Seek(0, 0);
            return ProtoBuf.Serializer.Deserialize<TTestObject>(memoryStream);
        }
        
        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();

            ProtoBuf.Serializer.Serialize(stream, obj);

            return stream;
        }

        public void Dispose()
        {
            memoryStream.Dispose();
        }
    }
}