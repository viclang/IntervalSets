using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntervalExtensions.Tests
{
    public class IntervalIntegerTests
    {
        [Fact]
        public void Test()
        {
            var interval = new Interval<int>(10,5);
            var string1 = interval.ToString();
        }
    }
}
