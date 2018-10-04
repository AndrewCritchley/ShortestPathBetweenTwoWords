using FluentAssertions;
using NUnit.Framework;

namespace WordDistanceTechnicalTest.Domain.Tests.Unit.Fixtures.ShortestPathCalculatorTests
{
    [TestFixture]
    public class DetermineShortestPathFixture
    {
        [Test]
        public void It_Should_Determine_The_Shortest_Path_Between_Two_Words_Using_Only_Words_In_The_Dictionary()
        {
            // Arrange
            var wordDictionary = new[]
            {
                "Spin",
                "Spit",
                "Spat",
                "Spot",
                "Span"
            };

            var expected = new[]
            {
                "Spin",
                "Spit",
                "Spot"
            };

            var startWord = "Spin";
            var endWord = "Spot";

            var serviceUnderTest = new ShortestPathCalculator();

            // Act
            var actual = serviceUnderTest.GetShortestPathFromStartWordToEndWord(startWord, endWord, wordDictionary);

            // Assert
            actual.Should().BeEquivalentTo(expected, "This is the shortest path from the start word to the end word");
        }
    }
}
