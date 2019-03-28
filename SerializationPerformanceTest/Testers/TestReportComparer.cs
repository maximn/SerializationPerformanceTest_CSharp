using System.Collections.Generic;

namespace SerializationPerformanceTest.Testers
{
    public class TestReportComparer : IComparer<TestReport>
    {
        public TestReportComparer(double seralizeWeight = 1, double deserializeWeight = 8, double sizeWeight = 0)
        {
            SeralizeWeight = seralizeWeight;
            DeserializeWeight = deserializeWeight;
            SizeWeight = sizeWeight;
        }

        public double SeralizeWeight { get; }
        public double DeserializeWeight { get; }
        public double SizeWeight { get; }


        public int Compare(TestReport x, TestReport y)
        {
            var scoreX = 
                x.Deserialization.Ticks * DeserializeWeight + 
                x.Serialization.Ticks * DeserializeWeight + 
                (long)x.StreamSize * SizeWeight;

            var scoreY =
                y.Deserialization.Ticks * DeserializeWeight +
                y.Serialization.Ticks * DeserializeWeight +
                (long)x.StreamSize * SizeWeight;

            return scoreX.CompareTo(scoreY);
        }
    }
}
