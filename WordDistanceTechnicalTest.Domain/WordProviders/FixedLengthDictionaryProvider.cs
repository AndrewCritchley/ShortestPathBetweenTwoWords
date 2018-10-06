using System.Collections.Generic;
using System.Linq;

namespace WordDistanceTechnicalTest.Domain.WordProviders
{
    /// <summary>
    /// Decorator pattern. I've used this because the spec specifies we should only consider four letter words,
    ///     while the sample file has other lengths. Due to this perceived ambiguity I opted to push this burden onto the DI wireup.
    /// </summary>
    public class FixedLengthDictionaryProvider : IDictionaryProvider
    {
        private readonly IDictionaryProvider _dictionaryProvider;
        private readonly int _length;

        public FixedLengthDictionaryProvider(IDictionaryProvider dictionaryProvider, int length)
        {
            _dictionaryProvider = dictionaryProvider;
            _length = length;
        }

        public IReadOnlyCollection<string> GetWords()
        {
            var allWords = _dictionaryProvider.GetWords();

            var wordsWithSpecifiedLength = allWords.Where(e => e.Length == _length).ToList();

            //TODO: This should log out the original length and the filtered length. 

            return wordsWithSpecifiedLength;

        }
    }
}
