using System;
using System.Diagnostics;
using System.IO;

namespace SerializationPerformanceTest.Testers
{
    abstract public class SerializationTester : IDisposable
    {
        protected MemoryStream MemoryStream { get; set; }

        public abstract TestReport Test(int iterations = 100);

        public virtual void Dispose() => MemoryStream.Dispose();
    }

    /// <summary>
    /// Base class for testing serialization formats/frameworks.
    /// </summary>
    /// <typeparam name="TTestObject">The object that will be tested to Serialize/Deserialize</typeparam>
    abstract public class SerializationTester<TTestObject> : SerializationTester
    {
        protected TTestObject TestObject { get; private set; }

        private bool isInit;

        protected SerializationTester(TTestObject testObject)
        {
            base.MemoryStream = new MemoryStream();
            this.TestObject = testObject;
        }

        /// <summary>
        /// Will do any preparations needed before serializing/deserializing
        /// </summary>
        protected virtual void Init()
        {
            isInit = true;
            MemoryStream = Serialize();
        }

        /// <summary>
        /// Will deserialize the TestObject to a .NET Object 
        /// </summary>
        /// <returns></returns>
        protected abstract TTestObject Deserialize();

        /// <summary>
        /// Will serialize the object to a MemoryStream
        /// </summary>
        /// <returns></returns>
        protected abstract MemoryStream Serialize();

        /// <summary>
        /// Will run the tests for Size/Speed of Serialization/Deserialization
        /// </summary>
        /// <param name="iterations"></param>
        public override TestReport Test(int iterations = 100)
        {
            if (!isInit)
            {
                Init();
            }

            var deserizalization = Measure<TTestObject>(this.Deserialize, iterations);
            GC.Collect();

            var serialzation = Measure<MemoryStream>(this.Serialize, iterations);
            GC.Collect();

            return new TestReport(MemoryStream.Length, serialzation, deserizalization, iterations, GetType());
        }

        private TimeSpan Measure<TTest>(Func<TTest> testFunc, int iterations)
        {
            var list = new TTest[iterations];

            //warm up lazy initialized classes
            TTest warmup = testFunc.Invoke();

            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                list[i] = testFunc.Invoke();
            }

            sw.Stop();

            GC.KeepAlive(warmup);

            return sw.Elapsed;
        }


    }
}
