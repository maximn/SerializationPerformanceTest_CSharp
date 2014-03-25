using System.IO;
using ServiceStack.Text;

namespace SerializationPerformanceTest.Testers
{
    class JsonServiceStackSerializationTester<TTestObject> : SerializationTester<TTestObject>
    {
        private string text;
        private TypeSerializer<TTestObject> serializer;


        public JsonServiceStackSerializationTester(string sourceDataFilename)
            : base(sourceDataFilename)
        {
        }

        protected override void Init()
        {
            text = File.ReadAllText(this.SourceDataFilename);
            serializer = new TypeSerializer<TTestObject>();
        }

        protected override TTestObject Deserialize()
        {
            return serializer.DeserializeFromString(text);
        }

        protected override MemoryStream Serialize(TTestObject obj)
        {
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            serializer.SerializeToWriter(obj, streamWriter);
            streamWriter.Flush();

            return stream;
        }
    }
}