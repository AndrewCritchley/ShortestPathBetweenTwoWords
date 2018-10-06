using System.Collections.Generic;
using System.IO;

namespace WordDistanceTechnicalTest.Domain.ResultProcessors
{
    public class OutputToTextFileResultsProcessor : IResultProcessor
    {
        public void ProcessResults(string outputFilePath, IReadOnlyCollection<string> results)
        {
            var fileInfo = new FileInfo(outputFilePath);

            if (!fileInfo.Exists)
                Directory.CreateDirectory(fileInfo.Directory.FullName);

            File.WriteAllLines(outputFilePath, results);
        }
    }
}
