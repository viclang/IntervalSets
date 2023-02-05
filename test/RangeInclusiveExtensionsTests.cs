using FluentAssertions;
using RangeExtensions.Interfaces;
using RangeExtensions.Tests.Extensions;
using RangeExtensions.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RangeExtensions.Tests.RangeInclusive
{
    public class RangeInclusiveExtensionsTests
    {
        [Theory]
        [ClassData(typeof(RangeInclusiveTestData.Overlapping))]
        public void OverlapsWith_Should_BeTrue(RangeInclusiveStub value)
        {
            //act
            var actual = value.OverlapsWith(RangeInclusiveTestData.RelativeRange);
            value.ToExclusive();

            //assert
            actual.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(RangeInclusiveTestData.NonOverlapping))]
        public void OverlapsWith_Should_BeFalse(RangeInclusiveStub value)
        {
            //act
            var actual = value.OverlapsWith(RangeInclusiveTestData.RelativeRange);

            //assert
            actual.Should().BeFalse();
        }

        [Fact]
        public void Cast()
        {
            var ex = RangeInclusiveTestData.RelativeRange.ToExclusive();
            //var inw = RangeExclusiveTestData.RelativeRange.ToInclusive();

            var actual = new RangeInclusiveTestData.NonOverlapping().AsEnumerable<IRangeInclusive<int>>();
        }
    }
}
