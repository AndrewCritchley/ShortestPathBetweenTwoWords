using FluentAssertions;
using NUnit.Framework;
using WordDistanceTechnicalTest.Domain.Exceptions;
using WordDistanceTechnicalTest.Domain.PathCalculators;

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

            var serviceUnderTest = new BreadthFirstShortestPathCalculator();

            // Act
            var actual = serviceUnderTest.GetShortestPathFromStartWordToEndWord(startWord, endWord, wordDictionary);

            // Assert
            actual.Should().BeEquivalentTo(expected, "This is the shortest path from the start word to the end word");
        }

        [Test]
        public void It_Should_Return_The_Word_If_The_StartWord_And_EndWord_Match()
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
                "Spin"
            };

            var startWord = "Spin";
            var endWord = "Spin";

            var serviceUnderTest = new BreadthFirstShortestPathCalculator();

            // Act
            var actual = serviceUnderTest.GetShortestPathFromStartWordToEndWord(startWord, endWord, wordDictionary);

            // Assert
            actual.Should().BeEquivalentTo(expected, "The start and end words match - no path between them is needed.");
        }

        [Test]
        public void It_Should_Throw_An_Exception_When_No_Path_Is_Possible()
        {
            // Arrange
            var wordDictionary = new[]
            {
                "Car",
                "Bus",
                "Bike",
                "Motorbike"
            };

            var startWord = "Car";
            var endWord = "Bus";

            var serviceUnderTest = new BreadthFirstShortestPathCalculator();

            // Act / Assert
            serviceUnderTest.Invoking(e => e.GetShortestPathFromStartWordToEndWord(startWord, endWord, wordDictionary))
                .Should()
                .Throw<GraphPathNotFoundException>();
        }
    }
}
