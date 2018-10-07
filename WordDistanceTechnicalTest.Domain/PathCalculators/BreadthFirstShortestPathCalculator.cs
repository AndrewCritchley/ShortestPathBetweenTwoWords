using System;
using System.Collections.Generic;
using System.Linq;
using WordDistanceTechnicalTest.Domain.Exceptions;
using WordDistanceTechnicalTest.Domain.Extensions;
using WordDistanceTechnicalTest.Domain.PathCalculators.Models;

namespace WordDistanceTechnicalTest.Domain.PathCalculators
{
    /// <summary>
    /// Calculates the shortest path between two words from a given set of words using a graph.
    /// </summary>
    public class BreadthFirstShortestPathCalculator : IShortestPathCalculator
    {
        public IReadOnlyCollection<string> GetShortestPathFromStartWordToEndWord(string startWord, string endWord, IReadOnlyCollection<string> dictionaryWords)
        {
            // If the start word is the end word then it is also the path to iself.
            if (startWord.Equals(endWord, StringComparison.InvariantCultureIgnoreCase))
                return new[] { startWord };

            var graph = GetAllDictionaryWordsAsGraphItems(dictionaryWords);
            var startWordGraphItem = graph.SingleOrDefault(e => e.Word.Equals(startWord, StringComparison.InvariantCultureIgnoreCase));
            var endWordGraphItem = graph.SingleOrDefault(e => e.Word.Equals(endWord, StringComparison.InvariantCultureIgnoreCase));
            var shortestPath = default(GraphPath);

            if (startWordGraphItem == null)
                throw new ArgumentException($"Start word is not present in dictionary", nameof(startWord));
            if (endWordGraphItem == null)
                throw new ArgumentException($"End word is not present in dictionary", nameof(endWord));

            var pathsQueue = new Queue<GraphPath>(GetInitialPaths(startWordGraphItem));

            while (pathsQueue.Any())
            {
                var path = pathsQueue.Dequeue();

                if (path.CurrentPath.Last() == endWordGraphItem)
                {
                    shortestPath = path;
                    // we can now remove any other possible paths from the queue which have a length which is equal to or greater than the path we already found
                    //      because we are just going with the first path we find at a given length.
                    pathsQueue = pathsQueue.RemovePathsWithALengthEqualOrGreaterThan(shortestPath.CurrentPath.Count);
                }
                else if (shortestPath == null || IsCurrentPathChildrenStillShorterThanTheShortestPathAlreadyFound(path, shortestPath))
                {
                    foreach (var unvisitedPath in path.CreateUnvisitedPaths())
                        pathsQueue.Enqueue(unvisitedPath);
                }
            }

            if (shortestPath == null)
                throw new GraphPathNotFoundException(startWord, endWord);

            return shortestPath.AsStrings();
        }

        /// <summary>
        /// Validates that if we keep traversing the next set of child nodes from the current path we haven't exceeded the length of the shortest path.
        /// </summary>
        /// <returns></returns>
        private bool IsCurrentPathChildrenStillShorterThanTheShortestPathAlreadyFound(GraphPath currentPath, GraphPath shortestPath)
        {
            return (currentPath.CurrentPath.Count + 1) <= shortestPath.CurrentPath.Count;
        }

        private IReadOnlyCollection<GraphPath> GetInitialPaths(GraphItem graphItem)
        {
            var initialPaths = graphItem.WordsWithASingleChange
                .Select(e => graphItem + e)
                .ToList();

            return initialPaths;
        }

        private IReadOnlyCollection<GraphItem> GetAllDictionaryWordsAsGraphItems(IReadOnlyCollection<string> dictionaryWords)
        {
            var allGraphItems = dictionaryWords.Select(e => new GraphItem(e)).ToList();

            foreach (var graphItem in allGraphItems)
                graphItem.SetWordsWithASingleChange(allGraphItems);

            return allGraphItems;
        }
    }
}
