using Qowaiv.IO;
using System;
using System.Text;

namespace SerializationPerformanceTest.Testers
{
    public class TestReport
    {
        public TestReport(StreamSize streamSize, TimeSpan serialization, TimeSpan deserizalization, int iterations, Type seralizer)
        {
            StreamSize = streamSize;
            Serialization = serialization;
            Deserialization = deserizalization;
            Iterations = iterations;
            SeralizerType = seralizer;
        }

        public StreamSize StreamSize { get; }

        public TimeSpan Serialization { get; }

        public TimeSpan Deserialization { get; }

        public Type SeralizerType { get; }

        public int Iterations { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendType(sb, SeralizerType);
            sb.AppendLine();
            sb.AppendLine($"Size: {StreamSize: F}");
            sb.AppendLine($"Serialize:   {Serialization.Ticks / (double)Iterations:#,##0.0} Ticks");
            sb.AppendLine($"Deserialize: {Deserialization.Ticks / (double)Iterations:#,##0.0} Ticks");
            return sb.ToString();
        }

        private static void AppendType(StringBuilder sb, Type type)
        {
            if(type.IsGenericType)
            {
                var root = type.Name.Substring(0, type.Name.IndexOf('`'));

                sb.Append(root);

                var args = type.GetGenericArguments();

                sb.Append("<");
                AppendType(sb, args[0]);
                for(var i = 1; i < args.Length;i++)
                {
                    sb.Append(", ");
                    AppendType(sb, args[i]);
                }
                sb.Append(">");
            }
            else
            {
                sb.Append(type.Name);
            }
        }
    }
}
