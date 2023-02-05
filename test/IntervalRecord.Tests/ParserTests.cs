
using System;

namespace IntervalRecord.Tests
{
    public class ParserTests
    {
        private const string validString = "3537asd([1,2])))), ])[(4,5],asdasdlkjl[[6,7)][,][][.](6,)[,7](8,][9,)";
        private const string InvalidString = "3537asd([1.2])))), ])[(4.5],asdasdlkjl[[6.7)][.][][.](6.)[.7](8.][9.)";

        public static TheoryData<string, Interval<int>?> ValidStringToParseWithExpectedResults = new TheoryData<string, Interval<int>?>
        {
            { "[,]", new Interval<int>() },
            { "[,)", new Interval<int>() },
            { "(,]", new Interval<int>() },
            { "(,)", new Interval<int>() },
            { "[1,2]", new Interval<int>(1, 2, true, true) },
            { "[1,2)", new Interval<int>(1, 2, true, false ) },
            { "(1,2]", new Interval<int>(1, 2, false, true ) },
            { "(1,2)", new Interval<int>(1, 2, false, false ) },
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
            // Act
            var act = () => Interval.Parse<int>(stringToParse);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Interval not found in string. Please provide an interval string in correct format");
        }

        [Theory]
        [InlineData("[1.0,2.0]")]
        [InlineData("[03/08/2022, 03/08/2022]")]
        [InlineData("[03-08-2022, 03-08-2022]")]
        public void Parse_ShouldThrowFormatException(string stringToParse)
        {
            // Act
            var act = () => Interval.Parse<int>(stringToParse);

            // Assert
            act.Should()
                .Throw<FormatException>()
                .WithMessage("Input string was not in a correct format.");
        }


        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        public void Parse_ShouldBeExpectedResult(string stringToParse, Interval<int> expectedResult)
        {
            // Act
            var result = Interval.Parse<int>(stringToParse);

            // Assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        [MemberData(nameof(InvalidStringToParseWithNull))]
        public void TryParse_ShouldBeExpectedResult(string stringToParse, Interval<int>? expectedResult)
        {
            // Act
            var isValid = Interval.TryParse<int>(stringToParse, out var result);

            // Assert
            isValid.Should().Be(expectedResult is not null);
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void ParseAll_ShouldBeEmpty()
        {
            // Act
            var result = Interval.ParseAll<int>(InvalidString);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ParseAll_ShouldHaveCount()
        {
            // Act
            var result = Interval.ParseAll<int>(validString);

            // Assert
            result.Should().HaveCount(8);
        }
    }
}
