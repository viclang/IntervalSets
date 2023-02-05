using RangeExtensions.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RangeExtensions.Tests.RangeInclusive
{
    public abstract class RangeInclusiveTestData
    {
        public static readonly RangeInclusiveStub RelativeRange = new RangeInclusiveStub(3, 5);

        public class NonOverlapping : TheoryData<RangeInclusiveStub>
        {
            public static readonly RangeInclusiveStub Before = new RangeInclusiveStub(1, 2);
            public static readonly RangeInclusiveStub After = new RangeInclusiveStub(6, 7);
            public static readonly RangeInclusiveStub AfterNull = new RangeInclusiveStub(6, null);

            public NonOverlapping()
            {
                Add(Before);
                Add(After);
                Add(AfterNull);
            }
        }

        public class Overlapping : TheoryData<RangeInclusiveStub>
        {
            public static readonly RangeInclusiveStub FromInside = new RangeInclusiveStub(2, 3);
            public static readonly RangeInclusiveStub ToInside = new RangeInclusiveStub(5, 6);
            public static readonly RangeInclusiveStub InsideNull = new RangeInclusiveStub(4, null);
            public static readonly RangeInclusiveStub Inside = new RangeInclusiveStub(4, 4);
            public static readonly RangeInclusiveStub FromNull = new RangeInclusiveStub(3, null);
            public static readonly RangeInclusiveStub ToNull = new RangeInclusiveStub(5, null);
            public static readonly RangeInclusiveStub BeforeNull = new RangeInclusiveStub(4, null);

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
