using System.Collections.Generic;

namespace WordDistanceTechnicalTest.Domain.PathCalculators
{
    public interface IShortestPathCalculator
    {
        IReadOnlyCollection<string> GetShortestPathFromStartWordToEndWord(string startWord, string endWord, IReadOnlyCollection<string> dictionaryWords);
    }
}
