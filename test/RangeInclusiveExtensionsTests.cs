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
            var actual = rangeA.OverlapsWith(rangeB);

            //assert
            actual.Should().BeTrue();
        }

        [MemberData(nameof(NotOverlappingRanges))]
        [Theory]
        public void OverlapsWith_Should_BeFalse(RangeStub rangeA, RangeStub rangeB)
        {
            //act
            var actual = rangeA.OverlapsWith(rangeB);

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

        public static TheoryData<RangeStub, RangeStub> NotOverlappingRanges = new()
        {
            {   //Before
                new RangeStub(1, 2),
                new RangeStub(4, 5)
            },
            {
                //From Touching
                new RangeStub(1, 3),
                new RangeStub(4, 5)
            },
            {
                //After
                new RangeStub(7, 8),
                new RangeStub(2, 5)
            },
            {
                //To Touching
                new RangeStub(6, 8),
                new RangeStub(2, 5)
            },
            {
                //To null
                new RangeStub(6, null),
                new RangeStub(2, 5)
            },
            {
                //To null
                new RangeStub(2, 5),
                new RangeStub(6, null)                
            }
        };
    }
}
