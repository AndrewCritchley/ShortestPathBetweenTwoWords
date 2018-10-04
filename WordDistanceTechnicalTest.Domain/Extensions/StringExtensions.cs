using System;
using System.Linq;

namespace WordDistanceTechnicalTest.Domain.Extensions
{
    public static class StringExtensions
    {
        public static int GetWordDistanceTo(this string startWord, string endWord)
        {
            var differenceCount = 0;

            for (var i = 0; i < Math.Max(startWord.Length, endWord.Length); i++)
            {
                var startLetter = startWord.ElementAtOrDefault(i);
                var endLetter = endWord.ElementAtOrDefault(i);

                if (startLetter != endLetter)
                    differenceCount++;
            }

            return differenceCount;
        }
    }
}
