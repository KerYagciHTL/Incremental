using System.Globalization;

namespace Incremental.Tests;

using BigNum;

public class BigNumTests
{
    public class StringToBigNumTests()
    {
        [Theory]
        [InlineData("250.75 K", 250.75, Suffix.K)]
        [InlineData("+250.75 K", 250.75, Suffix.K)]
        [InlineData("-123.45", -123.45, Suffix.None)]
        [InlineData("999.99 Qd", 999.99, Suffix.Qd)]
        [InlineData("1.00 De", 1.00, Suffix.De)]
        [InlineData("123.456", 123.456, Suffix.None)]
        [InlineData("0", 0, Suffix.None)]
        public void ToBigNum_ValidInputs_ShouldParseCorrectly(
            string input,
            double expectedNumber,
            Suffix expectedSuffix)
        {
            // Arrange
            var expected = new BigNum(expectedNumber, expectedSuffix);

            // Act
            var actual = new BigNum(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }

    public class BigNumToStringTests
    {
        public BigNumToStringTests()
        {
            // ensure consistent formatting regardless of machine locale
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Theory]
        // number        ↴   suffix         ↴      expected string
        [InlineData(250.75, Suffix.None, "250.75")]
        [InlineData(250.75, Suffix.K, "250.75 K")]
        [InlineData(0, Suffix.None, "0")]
        [InlineData(-123.45, Suffix.None, "-123.45")]
        [InlineData(-123.45, Suffix.T, "-123.45 T")]
        [InlineData(1000, Suffix.M, "1000 M")]
        public void ToString_FormatsCorrectly(double number, Suffix suffix, string expected)
        {
            // Arrange
            var bn = new BigNum(number, suffix);

            // Act
            var actual = bn.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        // round-trip: ToString → new BigNum(string) should yield the same value
        [InlineData(435.21, Suffix.None)]
        [InlineData(435.21, Suffix.M)]
        [InlineData(-999.99, Suffix.Qd)]
        [InlineData(0.5, Suffix.None)]
        public void ToString_RoundTrip_ShouldParseBackToSameValue(double number, Suffix suffix)
        {
            // Arrange
            var original = new BigNum(number, suffix);

            // Act
            var str = original.ToString();
            var reparsed = new BigNum(str);

            // Assert
            Assert.Equal(original, reparsed);
        }

        [Fact]
        public void ToString_SingleDecimalZero_ShouldNotIncludeTrailingDotZero()
        {
            // e.g. 1.0 → "1"
            var bn = new BigNum(1.0, Suffix.None);

            Assert.Equal("1", bn.ToString());
        }
    }

    public class BigNumOperatorTests
    {
        // + operator

        [Fact]
        public void Add_NoOverflow_ShouldKeepSuffix()
        {
            // Arrange
            var a = new BigNum(200, Suffix.None);
            var b = new BigNum(300, Suffix.None);

            // Act
            var result = a + b;

            // Assert
            Assert.Equal(new BigNum(500, Suffix.None), result);
        }

        [Fact]
        public void Add_SingleOverflow_ShouldIncrementSuffixOnce()
        {
            // 600 K + 500 K = 1100 → 1.1 M
            var a = new BigNum(600, Suffix.K);
            var b = new BigNum(500, Suffix.K);

            var result = a + b;

            Assert.Equal(new BigNum(1.1, Suffix.M), result);
        }

        [Fact]
        public void Add_DoubleOverflow_ShouldIncrementSuffixTwice()
        {
            // 1_100_000 K + 0 K = 1_100_000 →
            // 1_100_000/1_000=1_100 → suffix K→M
            // 1_100/1_000  =1.1   → suffix M→B
            //TODO Fix Unit Tests wtf is that?
            var a = new BigNum(1_100_000, Suffix.K);
            var b = new BigNum(0, Suffix.K);

            var result = a + b;

            Assert.Equal(new BigNum(1.1, Suffix.B), result);
        }


        // - operator

        [Fact]
        public void Subtract_NoUnderflow_ShouldKeepSuffix()
        {
            // 5 M - 3 M = 2 M
            var a = new BigNum(5, Suffix.M);
            var b = new BigNum(3, Suffix.M);

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(2, Suffix.M), result);
        }

        [Fact]
        public void Subtract_SingleUnderflow_ShouldDecrementSuffixOnce()
        {
            // 0.5 M - 0.2 M = 0.3→0.3*1_000=300, suffix M→K
            var a = new BigNum(0.5, Suffix.M);
            var b = new BigNum(0.2, Suffix.M);

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(300, Suffix.K), result);
        }

        [Fact]
        public void Subtract_EqualValues_ShouldReturnZeroWithSameSuffix()
        {
            var a = new BigNum(250, Suffix.K);
            var b = new BigNum(250, Suffix.K);

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(0, Suffix.K), result);
        }

        [Fact]
        public void Subtract_ResultExactlyOne_ShouldKeepSuffix()
        {
            // 1.5 M - 0.5 M = 1.0 M → no underflow
            var a = new BigNum(1.5, Suffix.M);
            var b = new BigNum(0.5, Suffix.M);

            var (result, worked) = a - b;

            Assert.True(worked);
            Assert.Equal(new BigNum(1.0, Suffix.M), result);
        }
    }
}