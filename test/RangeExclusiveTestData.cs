using RangeExtensions.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RangeExtensions.Tests.RangeInclusive
{
    public abstract class RangeExclusiveTestData
    {
        public static readonly RangeStub RelativeRange = new RangeStub(3, 6);

        public class NonOverlapping : TheoryData<RangeStub>
        {
            public static readonly RangeStub Before = new RangeStub(1, 3);
            public static readonly RangeStub After = new RangeStub(6, 8);
            public static readonly RangeStub AfterNull = new RangeStub(6, null);

            public NonOverlapping()
            {
                Add(Before);
                Add(After);
                Add(AfterNull);
            }
        }

        public class Overlapping : TheoryData<RangeStub>
        {
            public static readonly RangeStub FromInside = new RangeStub(2, 4);
            public static readonly RangeStub ToInside = new RangeStub(5, 7);
            public static readonly RangeStub InsideNull = new RangeStub(5, null);
            public static readonly RangeStub Inside = new RangeStub(4, 5);
            public static readonly RangeStub FromNull = new RangeStub(3, null);
            public static readonly RangeStub ToNull = new RangeStub(5, null);
            public static readonly RangeStub BeforeNull = new RangeStub(4, null);

            public Overlapping()
            {
                Add(RelativeRange);
                Add(FromInside);
                Add(ToInside);
                Add(InsideNull);
                Add(Inside);
                Add(FromNull);
                Add(ToNull);
                Add(BeforeNull);
            }
        }
    }
}
