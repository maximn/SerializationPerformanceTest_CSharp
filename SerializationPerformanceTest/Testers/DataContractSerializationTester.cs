using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SerializationPerformanceTest.Testers
{
    class DataContractSerializationTester<TTestObject> : SerializationTester<TTestObject>, IDisposable
    {
        private DataContractSerializer serializer;
        private MemoryStream memoryStream;


        public DataContractSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            serializer = new DataContractSerializer(typeof(TTestObject));

            memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(base.SourceDataFilename, FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
        }

        protected override TTestObject Deserialize()
        {
            memoryStream.Seek(0, 0);
            return (TTestObject)serializer.ReadObject(memoryStream);
        }
        
        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();

            serializer.WriteObject(stream, obj);

            return stream;
        }

        public void Dispose()
        {
            memoryStream.Dispose();
        }
    }
}