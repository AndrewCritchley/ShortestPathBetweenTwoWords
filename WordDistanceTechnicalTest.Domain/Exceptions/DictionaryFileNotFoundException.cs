using System;

namespace WordDistanceTechnicalTest.Domain.Exceptions
{
    /// <summary>
    /// Thrown when the input file for the dictionary words is not found.
    /// </summary>
    public class DictionaryFileNotFoundException : Exception
    {
        public string FilePath { get; }

        public DictionaryFileNotFoundException(string filePath) : base($"Dictionary file '{filePath}' not found.")
        {
            FilePath = filePath;
        }
    }
}
