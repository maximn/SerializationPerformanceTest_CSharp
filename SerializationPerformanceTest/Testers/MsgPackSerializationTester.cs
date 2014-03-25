using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MsgPack.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class MsgPackSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private MessagePackSerializer<TTestObject> serializer;
        private byte[] data;


        public MsgPackSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            serializer = MessagePackSerializer.Create<TTestObject>();
            data = File.ReadAllBytes(base.SourceDataFilename);
        }

        protected override TTestObject Deserialize()
        {
            return serializer.UnpackSingleObject(data);
        }
        
        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();
            
            serializer.Pack(stream, obj);

            return stream;
        }

    }
    
}