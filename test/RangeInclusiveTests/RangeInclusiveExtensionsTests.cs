using FluentAssertions;
using RangeExtensions.Interfaces;
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
        [ClassData(typeof(RangeInclusiveTestData.Overlapping))]
        [Theory]
        public void OverlapsWith_Should_BeTrue(RangeInclusiveStub value)
        {
            //act
            var actual = value.OverlapsWith(RangeInclusiveTestData.RelativeRange);

            //assert
            actual.Should().BeTrue();
        }

        [ClassData(typeof(RangeInclusiveTestData.NonOverlapping))]
        [Theory]
        public void OverlapsWith_Should_BeFalse(RangeInclusiveStub value)
        {
            //act
            var actual = value.OverlapsWith(RangeInclusiveTestData.RelativeRange);

            //assert
            actual.Should().BeFalse();
        }
    }
}
