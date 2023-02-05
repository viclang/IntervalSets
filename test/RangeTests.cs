using FluentAssertions;
using RangeExtensions.Interfaces;
using RangeExtensions.Tests.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace RangeExtensions.Tests
{
    public class RangeTests
    {
        [MemberData(nameof(InclusiveHappyPath))]
        [Theory]
        public void HappyPath(RangeStub rangeToAdd)
        {
            //arrange
            var ranges = Create();

            //act
            ranges.Add(rangeToAdd);

            //assert

        }

        [MemberData(nameof(InclusiveSadPath))]
        [Theory]
        public void SadPath(RangeStub rangeToAdd)
        {
            //arrange
            var ranges = Create();

            //act
            var act = () => ranges.Add(rangeToAdd);

            //assert
            act.Should().Throw<NotSupportedException>();
        }

        public IList<RangeStub> Create()
        {
            return new List<RangeStub>
            {
                new RangeStub(3, 5),
                new RangeStub(6, 8),
                new RangeStub(9, null)
            };
        }

        public static TheoryData<RangeStub> InclusiveHappyPath => new()
        {
            new RangeStub(1, 2),
            new RangeStub(4, 5),
            new RangeStub(10, null)
        };

        public static TheoryData<RangeStub> InclusiveSadPath => new()
        {
            new RangeStub(1, 2),
            new RangeStub(2, 1),
            new RangeStub(3, 5),
            new RangeStub(2, 5)
        };
    }
}