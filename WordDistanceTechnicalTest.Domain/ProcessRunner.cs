using WordDistanceTechnicalTest.Domain.DictionaryProviders;
using WordDistanceTechnicalTest.Domain.PathCalculators;
using WordDistanceTechnicalTest.Domain.ResultProcessors;

namespace WordDistanceTechnicalTest.Domain
{
    public class ProcessRunner
    {
        private readonly IDictionaryProvider _dictionaryProvider;
        private readonly IShortestPathCalculator _shortestPathCalculator;
        private readonly IResultProcessor _resultProcessor;

        public ProcessRunner(IDictionaryProvider dictionaryProvider, IShortestPathCalculator shortestPathCalculator, IResultProcessor resultProcessor)
        {
            _dictionaryProvider = dictionaryProvider;
            _shortestPathCalculator = shortestPathCalculator;
            _resultProcessor = resultProcessor;
        }

        public void ProcessCommand(string startWord, string endWord, string outputFilePath)
        {
            var shortestPath =
                _shortestPathCalculator.GetShortestPathFromStartWordToEndWord(startWord, endWord,
                    _dictionaryProvider.GetWords());

            _resultProcessor.ProcessResults(outputFilePath, shortestPath);
        }
    }
}


