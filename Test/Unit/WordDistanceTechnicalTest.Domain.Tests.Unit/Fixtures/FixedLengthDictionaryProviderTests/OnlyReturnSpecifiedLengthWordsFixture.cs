using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WordDistanceTechnicalTest.Domain.WordProviders;

namespace WordDistanceTechnicalTest.Domain.Tests.Unit.Fixtures.FixedLengthDictionaryProviderTests
{
    [TestFixture]
    public class OnlyReturnSpecifiedLengthWordsFixture
    {
        [Test]
        public void It_Will_Only_Return_Words_Of_The_Specified_Length()
        {
            // Arrange
            var rawDictionary = new[]
            {
                "1",
                "22",
                "333",
                "4444",
                "four",
                "55555"
            };

            var expected = new[]
            {
                "4444",
                "four"
            };

            var dictionaryProvider = Substitute.For<IDictionaryProvider>();

            dictionaryProvider.GetWords().Returns(rawDictionary);

            var sut = new FixedLengthDictionaryProvider(dictionaryProvider, 4);

            // Act
            var actual = sut.GetWords();

            // Assert
            actual.Should().BeEquivalentTo(expected, "These are the only four letter words");
        }
    }
}
