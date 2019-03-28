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

            var testers = new SerializationTester[]
            {
                //List of beers
                new DataContractSerializationTester<List<Beer>>(beers),
                new XmlSerializationTester<List<Beer>>(beers),
                new BinarySerializationTester<List<Beer>>(beers),
                new JsonNewtonsoftSerializationTester<List<Beer>>(beers),
                new JsonServiceStackSerializationTester<List<Beer>>(beers),
                new ProtobufSerializationTester<List<Beer>>(beers),
                new MsgPackSerializationTester<List<Beer>>(beers),
                new ZeroFormatterTester<List<Beer>>(beers),
                
                //Single beer
                new DataContractSerializationTester<Beer>(beer),
                new XmlSerializationTester<Beer>(beer),
                new BinarySerializationTester<Beer>(beer),
                new JsonNewtonsoftSerializationTester<Beer>(beer),
                new JsonServiceStackSerializationTester<Beer>(beer),
                new ProtobufSerializationTester<Beer>(beer),
                new MsgPackSerializationTester<Beer>(beer),
                new ZeroFormatterTester<Beer>(beer),
            };

            foreach (var tester in testers)
            {
                using (tester)
                {
                    tester.Test();

                    Console.WriteLine();
                }

                GC.Collect();
            }
        }
    }
}
