using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordDistanceTechnicalTest.Domain.Exceptions;

namespace WordDistanceTechnicalTest.Domain.DictionaryProviders
{
    
    public class TextFileDictionaryProvider : IDictionaryProvider
    {
        private readonly Lazy<IReadOnlyCollection<string>> _words;

        public TextFileDictionaryProvider(string filePath)
        {
            _words = new Lazy<IReadOnlyCollection<string>>(() => LoadWords(filePath));
        }

        public IReadOnlyCollection<string> GetWords()
        {
            return _words.Value;
        }

        private IReadOnlyCollection<string> LoadWords(string filePath)
        {
            if (!File.Exists(filePath))
                throw new DictionaryFileNotFoundException(filePath);

            return File.ReadAllLines(filePath)
                    .OrderBy(e => e)
                    .ToList();
        }
    }
}
