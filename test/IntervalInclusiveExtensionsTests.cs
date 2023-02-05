//using FluentAssertions;
//using IntervalExtensions.Interfaces;
//using IntervalExtensions.Tests.Extensions;
//using IntervalExtensions.Tests.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace IntervalExtensions.Tests.IntervalInclusive
//{
//    public class IntervalInclusiveExtensionsTests
//    {
//        [Theory]
//        [ClassData(typeof(IntervalInclusiveTestData.Overlapping))]
//        public void OverlapsWith_Should_BeTrue(IntervalInclusiveStub other)
//        {
//            //act
//            var actual = other.OverlapsWith(IntervalInclusiveTestData.RelativeInterval, true);
//            var test = new DateOnly(2022, 3, 9);
//            test.AddDays(0);
//            //assert
//            actual.Should().BeTrue();
//        }

//        [Theory]
//        [ClassData(typeof(IntervalInclusiveTestData.NonOverlapping))]
//        public void OverlapsWith_Should_BeFalse(IntervalInclusiveStub other)
//        {
//            //act
//            var actual = other.OverlapsWith(IntervalInclusiveTestData.RelativeInterval, true);

//            //assert
//            actual.Should().BeFalse();
//        }
//    }
//}
