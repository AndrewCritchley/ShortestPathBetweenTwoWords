using System.Collections.Generic;

namespace WordDistanceTechnicalTest.Domain.ResultProcessors
{
    public interface IResultProcessor
    {
        void ProcessResults(string outputFilePath, IReadOnlyCollection<string> results);
    }
}
