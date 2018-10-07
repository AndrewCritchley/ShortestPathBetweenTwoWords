using System;

namespace WordDistanceTechnicalTest.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a path between the start and end words cannot be determined.
    /// </summary>
    public class GraphPathNotFoundException : Exception
    {
        public string StartWord { get; }
        public string EndWord { get; }

        public GraphPathNotFoundException(string startWord, string endWord)
            : base($"Could not find a path between the word '{startWord}' and '{endWord}'.")
        {
            StartWord = startWord;
            EndWord = endWord;
        }
    }
}
