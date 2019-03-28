using SerializationPerformanceTest.TestData.BelgianBeer;
using SerializationPerformanceTest.Testers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializationPerformanceTest
{
    internal static class Program
    {
        private static void Main()
        {
            var beers = BelgianBeerDataRetriever.GetDataFromXML();
            var beer = beers.First();

            var lists = new SerializationTester[]
            {
                //List of beers
                new DataContractSerializationTester<List<Beer>>(beers),
                new XmlSerializationTester<List<Beer>>(beers),
                new BinarySerializationTester<List<Beer>>(beers),
                new JsonJilSerializationTesterTester<List<Beer>>(beers),
                new JsonNewtonsoftSerializationTester<List<Beer>>(beers),
                new JsonServiceStackSerializationTester<List<Beer>>(beers),
                new ProtobufSerializationTester<List<Beer>>(beers),
                new MsgPackSerializationTester<List<Beer>>(beers),
                new ZeroFormatterTester<List<Beer>>(beers),
            };

            var singles = new SerializationTester[]
            {
                //Single beer
                new DataContractSerializationTester<Beer>(beer),
                new XmlSerializationTester<Beer>(beer),
                new BinarySerializationTester<Beer>(beer),
                new JsonJilSerializationTesterTester<Beer>(beer),
                new JsonNewtonsoftSerializationTester<Beer>(beer),
                new JsonServiceStackSerializationTester<Beer>(beer),
                new ProtobufSerializationTester<Beer>(beer),
                new MsgPackSerializationTester<Beer>(beer),
                new ZeroFormatterTester<Beer>(beer),
            };

            var comparer = new TestReportComparer();

            Console.WriteLine("Large sized");
            Test(lists, comparer);

            Console.WriteLine();
            Console.WriteLine("Small sized");
            Test(singles, comparer);

            Console.ReadLine();
        }

        private static void Test(IEnumerable<SerializationTester> lists, IComparer<TestReport> comparer)
        {
            var reports = new List<TestReport>();
            foreach (var tester in lists)
            {
                using (tester)
                {
                    reports.Add(tester.Test());
                }
                GC.Collect();
            }
            reports.Sort(comparer);

            foreach(var report in reports)
            {
                Console.WriteLine(report);
            }
        }
    }
}
