using FluentAssertions;
using RangeExtensions.Interfaces;
using RangeExtensions.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RangeExtensions.Tests
{
    public class RangeInclusiveExtensionsTests
    {
        [MemberData(nameof(OverlappingRanges))]
        [Theory]
        public void OverlapsWith_Should_BeTrue(RangeStub rangeA, RangeStub rangeB)
        {
            //act
            var actual = rangeA.OverlapsWithB(rangeB);

            //assert
            actual.Should().BeTrue();
        }

        [InlineData(1, 2, 4, 5)]//Before
        [InlineData(7, 8, 4, 5)]//After
        [InlineData(1, 3, 4, 5)]//From Touching        
        [InlineData(6, 8, 2, 5)]//To Touching
        [InlineData(6, null, 2, 5)]//To null
        [InlineData(2, 5, 6, null)]//To null
        [Theory]
        public void OverlapsWith_Should_BeFalse(int fromA, int? toA, int fromB, int? toB)
        {
            //arrange
            var rangeA = new RangeStub(fromA, toA);
            var rangeB = new RangeStub(fromB, toB);

            //act
            var actual = rangeA.OverlapsWithB(rangeB);

            //assert
            actual.Should().BeFalse();
        }

        public static TheoryData<RangeStub, RangeStub> OverlappingRanges = new()
        {
            {   //From inside
                new RangeStub(1, 2),
                new RangeStub(2, 5)
            },
            {
                //Inside From Touching
                new RangeStub(2, 6),
                new RangeStub(2, 5)
            },
            {
                //Enclosing From Touching
                new RangeStub(2, 3),
                new RangeStub(2, 5)
            },
            {
                //Enclosing
                new RangeStub(3, 4),
                new RangeStub(2, 5)
            },
            {
                //Enclosing To Touching
                new RangeStub(3, 5),
                new RangeStub(2, 5)
            },
            {
                //Exact Match
                new RangeStub(2, 5),
                new RangeStub(2, 5)
            },
            {
                //Inside
                new RangeStub(2, 5),
                new RangeStub(3, 4)
            },
            {
                //Inside To Touching
                new RangeStub(1, 5),
                new RangeStub(2, 5)
            },
            {
                //To Inside
                new RangeStub(5, 6),
                new RangeStub(2, 5)
            },
            {
                //To null
                new RangeStub(1, null),
                new RangeStub(2, 5)
            },
            {
                //From Touching To null
                new RangeStub(2, null),
                new RangeStub(2, 5)
            },
            {
                //From Inside To null
                new RangeStub(3, null),
                new RangeStub(2, 5)
            },
            {
                //From Inside To null
                new RangeStub(2, 5),
                new RangeStub(3, null)                
            },
            {
                //Both To null
                new RangeStub(2, null),
                new RangeStub(3, null)
            }
        };
    }
}
