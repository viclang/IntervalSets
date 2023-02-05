//using IntervalExtensions.Tests.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace IntervalExtensions.Tests.IntervalInclusive
//{
//    public abstract class IntervalInclusiveTestData
//    {
//        public static readonly IntervalInclusiveStub RelativeInterval = new IntervalInclusiveStub(3, 5);

//        public class NonOverlapping : TheoryData<IntervalInclusiveStub>
//        {
//            public static readonly IntervalInclusiveStub Before = new IntervalInclusiveStub(1, 2);
//            public static readonly IntervalInclusiveStub After = new IntervalInclusiveStub(6, 7);
//            public static readonly IntervalInclusiveStub AfterNull = new IntervalInclusiveStub(6, null);

//            public NonOverlapping()
//            {
//                Add(Before);
//                Add(After);
//                Add(AfterNull);
//            }
//        }

//        public class Overlapping : TheoryData<IntervalInclusiveStub>
//        {
//            public static readonly IntervalInclusiveStub StartInside = new(2, 3);
//            public static readonly IntervalInclusiveStub EndInside = new(5, 6);
//            public static readonly IntervalInclusiveStub InsideNull = new(4, null);
//            public static readonly IntervalInclusiveStub Inside = new(4, 4);
//            public static readonly IntervalInclusiveStub StartNull = new(3, null);
//            public static readonly IntervalInclusiveStub EndNull = new(5, null);
//            public static readonly IntervalInclusiveStub BeforeNull = new(4, null);

//            public Overlapping()
//            {
//                Add(RelativeInterval);
//                Add(StartInside);
//                Add(EndInside);
//                Add(InsideNull);
//                Add(Inside);
//                Add(StartNull);
//                Add(EndNull);
//                Add(BeforeNull);
//            }
//        }
//    }
//}
