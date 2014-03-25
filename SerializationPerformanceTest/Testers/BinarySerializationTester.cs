using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationPerformanceTest.Testers
{
    class BinarySerializationTester<TTestObject> : SerializationTester<TTestObject>, IDisposable
    {
        private IFormatter formatter;
        private MemoryStream memoryStream;

        public BinarySerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            formatter = new BinaryFormatter();

            memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(base.SourceDataFilename, FileMode.Open))
            {
                fileStream.CopyTo(memoryStream);
            }
        }

        protected override TTestObject Deserialize()
        {
            memoryStream.Seek(0, 0);
            TTestObject deserialize = (TTestObject)formatter.Deserialize(memoryStream);
            return deserialize;
        }


        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();
            formatter.Serialize(stream, obj);

            return stream;
        }

        public void Dispose()
        {
            memoryStream.Dispose();
        }
    }
}