using System;
using System.Configuration;

namespace WordDistanceTechnicalTest.Console.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public int? DictionaryWordAllowedLength { get; }

        public ApplicationConfiguration()
        {
            var dictionaryWordAllowedLengthString = GetOptionalValue(nameof(DictionaryWordAllowedLength);
            if (!string.IsNullOrWhiteSpace(dictionaryWordAllowedLengthString))
                DictionaryWordAllowedLength = int.Parse(dictionaryWordAllowedLengthString);
        }

        private string GetOptionalValue(string key)
        {
            var val = ConfigurationManager.AppSettings[key];
            return val;
        }
    }
}
