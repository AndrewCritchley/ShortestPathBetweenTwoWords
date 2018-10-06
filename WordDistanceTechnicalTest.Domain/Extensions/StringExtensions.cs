using System;
using System.Linq;

namespace WordDistanceTechnicalTest.Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool HasOnlySingleLetterDifference(this string startWord, string endWord)
        {
            var differenceCount = 0;

            for (var i = 0; i < Math.Max(startWord.Length, endWord.Length); i++)
            {
                var startLetter = startWord.ElementAtOrDefault(i);
                var endLetter = endWord.ElementAtOrDefault(i);

                if (startLetter != endLetter)
                    differenceCount++;

                // If we have more than one difference we can end this call early
                //  it's a bit of an early optimisation but performance was mentioned over the phone.
                if (differenceCount >= 2)
                    return false;
            }

            return true;
        }
    }
}
