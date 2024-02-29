namespace IntervalRecords.Experiment.Tests
{
    public class ParserTests
    {
        private const string validString = "3537asd([1,2])))), ])[(4,5],asdasdlkjl[[6,7)][∞,∞][][.](6,)[,7](8,][9,)";
        private const string InvalidString = "3537asd([1.2])))), ])[(4.5],asdasdlkjl[[6.7)][.][][.](6.)[.7](8.][9.)";

        public static TheoryData<string, Interval<int>?> ValidStringToParseWithExpectedResults = new TheoryData<string, Interval<int>?>
        {
            { "[,]", new Interval<int>(null, null, true, true) },
            { "[,)", new Interval<int>(null, null, true, false) },
            { "(,]", new Interval<int>(null, null, false, true) },
            { "(,)", Interval<int>.Unbounded },
            { "[1, 2]", new Interval<int>(1, 2, true, true) },
            { "[1, 2)", new Interval<int>(1, 2, true, false) },
            { "(1, 2]", new Interval<int>(1, 2, false, true) },
            { "(1, 2)", new Interval<int>(1, 2, false, false) },
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
            // Act
            var act = () => Interval<int>.Parse(stringToParse);

            // Assert
            act.Should()
                .Throw<FormatException>()
                .WithMessage("Interval not found in string. Please provide an interval string in correct format");
        }

        [Theory]
        [InlineData("[1.0,2.0]")]
        [InlineData("[03/08/2022, 03/08/2022]")]
        [InlineData("[03-08-2022, 03-08-2022]")]
        public void Parse_ShouldThrowFormatException(string stringToParse)
        {
            // Act
            var act = () => Interval<int>.Parse(stringToParse);

            // Assert
            act.Should()
                .Throw<FormatException>();
        }

        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        public void Parse_ShouldBeExpectedResult(string stringToParse, Interval<int> expectedResult)
        {
            // Act
            var result = Interval<int>.Parse(stringToParse);

            // Assert
            result.Should().Be(expectedResult);
        }


        [Theory]
        [MemberData(nameof(ValidStringToParseWithExpectedResults))]
        [MemberData(nameof(InvalidStringToParseWithNull))]
        public void TryParse_ShouldBeExpectedResult(string stringToParse, Interval<int>? expectedResult)
        {
            // Act
            var isValid = Interval<int>.TryParse(stringToParse, null, out var result);

            // Assert
            isValid.Should().Be(expectedResult is not null);
            result.Should().Be(expectedResult!);
        }

        [Fact]
        public void ParseAll_ShouldBeEmpty()
        {
            // Act
            var result = Interval<int>.ParseAll(InvalidString);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ParseAll_ShouldHaveCount()
        {
            // Act
            var result = Interval<int>.ParseAll(validString);

            // Assert
            result.Should().HaveCount(8);
        }
    }
}
