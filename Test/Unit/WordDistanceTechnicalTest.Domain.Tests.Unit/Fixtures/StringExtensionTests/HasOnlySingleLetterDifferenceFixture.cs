using FluentAssertions;
using NUnit.Framework;
using WordDistanceTechnicalTest.Domain.Extensions;

namespace WordDistanceTechnicalTest.Domain.Tests.Unit.Fixtures.StringExtensionTests
{
    [TestFixture]
    public class HasOnlySingleLetterDifferenceFixture
    {
        [Test]
        public void It_Will_Determine_A_Word_With_Only_Single_Change_To_Be_Valid()
        {
            // Arrage
            var inputWord = "ABC";
            var targetWord = "ABB";

            // Act
            var actual = inputWord.HasOnlySingleLetterDifference(targetWord);

            // Assert
            actual.Should().BeTrue();
        }

        [Test]
        public void It_Will_Determine_A_Word_With_Multiple_Changes_To_Be_Invalid()
        {
            // Arrage
            var inputWord = "ABC";
            var targetWord = "ADD";

            // Act
            var actual = inputWord.HasOnlySingleLetterDifference(targetWord);

            // Assert
            actual.Should().BeFalse();
        }

        [Test]
        public void It_Will_Determine_Words_With_A_Difference_In_Length_By_More_Than_One_To_Be_Invalid()
        {
            // Arrage
            var inputWord = "ABC";
            var targetWord = "ABCCC";

            // Act
            var actual = inputWord.HasOnlySingleLetterDifference(targetWord);

            // Assert
            actual.Should().BeFalse();
        }
    }
}
