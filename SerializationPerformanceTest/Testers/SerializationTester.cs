using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace SerializationPerformanceTest.Testers
{
    abstract class SerializationTester
    {
        public abstract void Test(int iterations = 100);
    }

    abstract class SerializationTester<TTestObject> : SerializationTester
    {
        protected readonly string SourceDataFilename;

        protected SerializationTester(string sourceDataFilename)
        {
            this.SourceDataFilename = sourceDataFilename;
        }

        /// <summary>
        /// Init everything needed for Deserialization/Serilization like Serializers, storeage for the data
        /// </summary>
        protected abstract void Init();
        protected abstract TTestObject Deserialize();

        protected abstract MemoryStream Serialize(TTestObject obj);


        // Must do the deseialization test first to have a sample object to serialize later.
        public override void Test(int iterations = 100)
        {
            this.Init();

            TimeSpan timeSpan;

            timeSpan = Measure<TTestObject>(this.Deserialize, iterations);
            Console.WriteLine(this.GetType().Name + "(D) : " + timeSpan.TotalMilliseconds / iterations);
            GC.Collect();

            var sample = Deserialize();
            timeSpan = Measure<MemoryStream>(() => this.Serialize(sample), iterations);

            Console.WriteLine(this.GetType().Name + "(S) : " + timeSpan.TotalMilliseconds / iterations);
            GC.Collect();
        }

        private TimeSpan Measure<TTestObject>(Func<TTestObject> testFunc, int iterations)
        {
            var list = new List<TTestObject>(iterations);

            //warm up lazy initialized classes
            TTestObject warmup = testFunc.Invoke();

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                TTestObject obj = testFunc.Invoke();

                list.Add(obj);
            }

            sw.Stop();

            GC.KeepAlive(warmup);

            return sw.Elapsed;
        }
    }
}