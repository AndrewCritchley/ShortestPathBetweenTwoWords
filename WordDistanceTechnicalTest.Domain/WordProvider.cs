using System;
using System.Collections.Generic;

namespace WordDistanceTechnicalTest.Domain
{
    public interface IDictionaryProvider
    {
        IReadOnlyCollection<string> GetWords();
    }

    public class TextFileDictionaryProvider : IDictionaryProvider
    {
        public IReadOnlyCollection<string> GetWords()
        {
            throw new NotImplementedException();
        }
    }
}
