namespace WordDistanceTechnicalTest.Console.Configuration
{
    public interface IApplicationConfiguration
    {
        /// <summary>
        /// Specifies the allowed length of a dictionary word.
        /// If specified it will filter all words in the dictionary to that length.
        /// </summary>
        int? DictionaryWordAllowedLength { get; }
    }
}
