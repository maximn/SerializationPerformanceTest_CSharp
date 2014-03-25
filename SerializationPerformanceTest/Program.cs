using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading;
using SerializationPerformanceTest.TestData.BelgianBeer;
using SerializationPerformanceTest.Testers;


namespace SerializationPerformanceTest
{

    internal static class Program
    {
        private static void Main()
        {
            //Go to the directory with the data
            Directory.SetCurrentDirectory(@".\..\..\TestData\BelgianBeer\Data");

            var testers = new SerializationTester[]
                {
                    //List of beers
                    new DataContractSerializationTester<List<Beer>>("beers.datacontract"), 
                    new XmlSerializationTester<List<Beer>>("beers.xml"),
                    new BinarySerializationTester<List<Beer>>("beers.bin"),
                    new JsonNewtonsoftSerializationTester<List<Beer>>("beers.jsonns"),
                    new JsonServiceStackSerializationTester<List<Beer>>("beers.jsonss"),
                    new ProtobufSerializationTester<List<Beer>>("beers.protobuf"),
                    new MsgPackSerializationTester<List<Beer>>("beers.msgpack"),
                    
                    //Single beer
                    new DataContractSerializationTester<Beer>("beer.datacontract"),
                    new XmlSerializationTester<Beer>("beer.xml"),
                    new BinarySerializationTester<Beer>("beer.bin"),
                    new JsonNewtonsoftSerializationTester<Beer>("beer.jsonns"),
                    new JsonServiceStackSerializationTester<Beer>("beer.jsonss"),
                    new ProtobufSerializationTester<Beer>("beer.protobuf"),
                    new MsgPackSerializationTester<Beer>("beer.msgpack"),                    
                };



            foreach (var tester in testers)
            {
                tester.Test();

                var disposable = tester as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                GC.Collect();
            }

            Console.WriteLine("Done.");
            Console.ReadKey();
        }
    }
}