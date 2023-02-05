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
        public static readonly RangeExclusiveStub RelativeRange = new RangeExclusiveStub(3, 6);

        public class NonOverlapping : TheoryData<RangeExclusiveStub>
        {
            public static readonly RangeExclusiveStub Before = new RangeExclusiveStub(1, 3);
            public static readonly RangeExclusiveStub After = new RangeExclusiveStub(6, 8);
            public static readonly RangeExclusiveStub AfterNull = new RangeExclusiveStub(6, null);

            public NonOverlapping()
            {
                Add(Before);
                Add(After);
                Add(AfterNull);
            }
        }

        public class Overlapping : TheoryData<RangeExclusiveStub>
        {
            public static readonly RangeExclusiveStub FromInside = new RangeExclusiveStub(2, 4);
            public static readonly RangeExclusiveStub ToInside = new RangeExclusiveStub(5, 7);
            public static readonly RangeExclusiveStub InsideNull = new RangeExclusiveStub(5, null);
            public static readonly RangeExclusiveStub Inside = new RangeExclusiveStub(4, 5);
            public static readonly RangeExclusiveStub FromNull = new RangeExclusiveStub(3, null);
            public static readonly RangeExclusiveStub ToNull = new RangeExclusiveStub(5, null);
            public static readonly RangeExclusiveStub BeforeNull = new RangeExclusiveStub(4, null);

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
