using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace SerializationPerformanceTest.Testers
{
    class XmlSerializationTester<TTestObject> : SerializationTester<TTestObject>, IDisposable
    {
        private XmlSerializer serializer;
        private MemoryStream memoryStream;

        public XmlSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            serializer = new XmlSerializer(typeof(TTestObject));
            memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(base.SourceDataFilename, FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
        }


        protected override TTestObject Deserialize()
        {
            memoryStream.Seek(0, 0);

            var deserialize = (TTestObject)serializer.Deserialize(memoryStream);
            return deserialize;
        }

        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();
            serializer.Serialize(stream, obj);

            return stream;
        }

        public void Dispose()
        {
            memoryStream.Dispose();            
        }
    }
}