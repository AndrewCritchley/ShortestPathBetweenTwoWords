using System;
using System.IO;
using WordDistanceTechnicalTest.Domain;
using WordDistanceTechnicalTest.Domain.Exceptions;

namespace WordDistanceTechnicalTest.Console
{
    internal class CommandLineInterface
    {
        public void RequestPathParamatersFromUser()
        {
            string dictionaryFilePath = PromptForDictionaryFilePath();
            string startWord = PromptForValue("Start Word");
            string endWord = PromptForValue("End Word");
            string outputFilePath = PromptForValue("Output File Path");

            ProcessPathRequest(dictionaryFilePath, startWord, endWord, outputFilePath);
        }

        private string PromptForDictionaryFilePath()
        {
            string dictionaryFilePath = default(string);
            do
            {
                if (dictionaryFilePath != null)
                    WriteConsoleLineInColour("Dictionary file path not found.", ConsoleColor.Red);

                dictionaryFilePath = PromptForValue("Dictionary File Path");
            } while (!File.Exists(dictionaryFilePath));

            return dictionaryFilePath;
        }

        private void WriteConsoleLineInColour(string message, System.ConsoleColor colour)
        {
            var originalForegroundColour = System.Console.ForegroundColor;
            System.Console.ForegroundColor = colour;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = originalForegroundColour;
        }

        private string PromptForValue(string prompt)
        {
            var inputValue = default(string);

            do
            {
                System.Console.Write($"{prompt}: ");
                inputValue = System.Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(inputValue));

            return inputValue;
        }

        private void ProcessPathRequest(string dictionaryFilePath, string startWord, string endWord, string outputFilePath)
        {
            var iocContainer = new FakeIoCContainer();
            var dictionaryProvider = iocContainer.ResolveDictionaryProvider(dictionaryFilePath);
            var shortestPathCalculator = iocContainer.ResolveShortestPathCalculator();
            var resultsProcessor = iocContainer.ResolveResultProcessor();

            var processRunner = new ProcessRunner(dictionaryProvider, shortestPathCalculator, resultsProcessor);

            try
            {
                processRunner.ProcessCommand(startWord, endWord, outputFilePath);
            }
            catch (GraphPathNotFoundException ex)
            {
                WriteConsoleLineInColour(ex.Message, ConsoleColor.Red);
            }
            catch (DictionaryFileNotFoundException ex)
            {
                WriteConsoleLineInColour(ex.Message, ConsoleColor.Red);
            }
        }
    }
}