using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ZeroFormatter;

namespace SerializationPerformanceTest.TestData.BelgianBeer
{
    [Serializable, ProtoContract, DataContract, ZeroFormattable]
    public class Beer
    {
        [ProtoMember(1), DataMember, Index(0)]
        public virtual string Brand { get; set; }

        [ProtoMember(2), DataMember, Index(1)]
        public virtual List<string> Sort { get; set; }

        [ProtoMember(3), DataMember, Index(2)]
        public virtual float Alcohol { get; set; }

        [ProtoMember(4), DataMember, Index(3)]
        public virtual string Brewery { get; set; }
    }
}
