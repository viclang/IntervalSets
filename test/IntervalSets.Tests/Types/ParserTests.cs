using FluentAssertions;
using IntervalSets.Types;

namespace IntervalSets.Tests.Types
{
    public class ParserTests
    {
        public static TheoryData<string, Interval<int>?> ValidStringToParseWithExpectedResults = new TheoryData<string, Interval<int>?>
        {
            { "(,)", Interval<int>.Unbounded },
            { "[1, 2]", new Interval<int>(1, 2, Bound.Closed, Bound.Closed) },
            { "[1, 2)", new Interval<int>(1, 2, Bound.Closed, Bound.Open) },
            { "(1, 2]", new Interval<int>(1, 2, Bound.Open, Bound.Closed) },
            { "(1, 2)", new Interval<int>(1, 2, Bound.Open, Bound.Open) },
        };

        public static TheoryData<string, Interval<int>?> InvalidStringToParseWithNull = new TheoryData<string, Interval<int>?>
        {
            { "[1.2]", null },
            { "[1..2]", null },
            { "[..]", null },
            { "[.]", null },
            { "[]", null },
        };

        [Theory]
        [InlineData("")]
        [InlineData("[1.2]")]
        [InlineData("[1..2]")]
        [InlineData("[..]")]
        [InlineData("[.]")]
        [InlineData("()")]
        [InlineData("[]")]
        [InlineData("(]")]
        [InlineData("[)")]
        [InlineData(",")]
        [InlineData(",,")]
        [InlineData("1,2")]
        [InlineData("],[")]
        [InlineData("],]")]
        [InlineData("[,[")]
        [InlineData("),(")]
        [InlineData("),)")]
        [InlineData("(,(")]
        [InlineData("{,}")]
        [InlineData("<,>")]
        public void Parse_ShouldThrowArgumentException(string stringToParse)
        {
            var actual = () => Interval<int>.Parse(stringToParse);

            actual.Should()
                .Throw<FormatException>()
                .WithMessage("Interval not found in string. Please provide an interval string in correct format.");
        }

        [Theory]
        [InlineData("[1.0,2.0]")]
        [InlineData("[03/08/2022, 03/08/2022]")]
        [InlineData("[03-08-2022, 03-08-2022]")]
        public void Parse_ShouldThrowFormatException(string stringToParse)
        {
            var actual = () => Interval<int>.Parse(stringToParse);

            actual.Should()
                .Throw<FormatException>();
        }

        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        public void Parse_ShouldBeExpectedResult(string stringToParse, Interval<int> expectedResult)
        {
            var result = Interval<int>.Parse(stringToParse);

            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        [MemberData(nameof(InvalidStringToParseWithNull))]
        public void TryParse_ShouldBeExpectedResult(string stringToParse, Interval<int>? expectedResult)
        {
            var isValid = Interval<int>.TryParse(stringToParse, null, out var result);

            isValid.Should().Be(expectedResult is not null);
            result.Should().Be(expectedResult!);
        }
    }
}
