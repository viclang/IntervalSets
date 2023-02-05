//using IntervalExtensions.Tests.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace IntervalExtensions.Tests.IntervalInclusive
//{
//    public abstract class IntervalExclusiveTestData
//    {
//        public static readonly IntervalExclusiveStub RelativeInterval = new IntervalExclusiveStub(3, 6);

//        public class NonOverlapping : TheoryData<IntervalExclusiveStub>
//        {
//            public static readonly IntervalExclusiveStub Before = new IntervalExclusiveStub(1, 3);
//            public static readonly IntervalExclusiveStub After = new IntervalExclusiveStub(6, 8);
//            public static readonly IntervalExclusiveStub AfterNull = new IntervalExclusiveStub(6, null);

//            public NonOverlapping()
//            {
//                Add(Before);
//                Add(After);
//                Add(AfterNull);
//            }
//        }

//        public class Overlapping : TheoryData<IntervalExclusiveStub>
//        {
//            public static readonly IntervalExclusiveStub StartInside = new IntervalExclusiveStub(2, 4);
//            public static readonly IntervalExclusiveStub EndInside = new IntervalExclusiveStub(5, 7);
//            public static readonly IntervalExclusiveStub InsideNull = new IntervalExclusiveStub(5, null);
//            public static readonly IntervalExclusiveStub Inside = new IntervalExclusiveStub(4, 5);
//            public static readonly IntervalExclusiveStub StartNull = new IntervalExclusiveStub(3, null);
//            public static readonly IntervalExclusiveStub EndNull = new IntervalExclusiveStub(5, null);
//            public static readonly IntervalExclusiveStub BeforeNull = new IntervalExclusiveStub(4, null);

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
