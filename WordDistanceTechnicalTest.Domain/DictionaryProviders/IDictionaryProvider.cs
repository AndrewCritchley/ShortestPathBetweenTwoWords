using System.Collections.Generic;

namespace WordDistanceTechnicalTest.Domain.DictionaryProviders
{
    public interface IDictionaryProvider
    {
        /// <summary>
        /// Returns a list of dictionary words in alphabetical order
        /// </summary>
        IReadOnlyCollection<string> GetWords();
    }
}
