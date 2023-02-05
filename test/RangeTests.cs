using System.Collections.Generic;
using Range.Tests.Models;
using Xunit;

namespace Range.Tests
{
    public class RangeTests
    {
        [Fact]
        public void HappyPath()
        {
            var ranges = new List<RangeStub>
            {
                //new RangeStub(1, 2),
                new RangeStub(1, 4),
                new RangeStub(5, 6),
                new RangeStub(7, 8),
                new RangeStub(9, 10),
                new RangeStub(11, null),
            };

            ranges.AddInclusive(new RangeStub(3,null));

            

        }
    }
}