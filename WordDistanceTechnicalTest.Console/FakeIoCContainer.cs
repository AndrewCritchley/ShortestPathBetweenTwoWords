using WordDistanceTechnicalTest.Console.Configuration;
using WordDistanceTechnicalTest.Domain.PathCalculators;
using WordDistanceTechnicalTest.Domain.ResultProcessors;
using WordDistanceTechnicalTest.Domain.WordProviders;

namespace WordDistanceTechnicalTest.Console
{
    public class FakeIoCContainer
    {
        public IApplicationConfiguration ResolveApplicationConfiguration()
        {
            return new ApplicationConfiguration();
        }

        public IDictionaryProvider ResolveDictionaryProvider(string dictionaryFilePath)
        {
            IApplicationConfiguration applicationConfiguration = ResolveApplicationConfiguration();
            IDictionaryProvider dictionaryProvider = new TextFileDictionaryProvider(dictionaryFilePath);

            if (applicationConfiguration.DictionaryWordAllowedLength.HasValue)
                dictionaryProvider = new FixedLengthDictionaryProvider(dictionaryProvider, applicationConfiguration.DictionaryWordAllowedLength.Value);

            return dictionaryProvider;
        }

        public IShortestPathCalculator ResolveShortestPathCalculator()
        {
            return new BreadthFirstShortestPathCalculator();
        }

        public IResultProcessor ResolveResultProcessor()
        {
            return new OutputToTextFileResultsProcessor();
        }
    }
}
