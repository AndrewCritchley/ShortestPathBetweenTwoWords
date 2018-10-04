using System;
using System.Collections.Generic;
using System.Linq;
using WordDistanceTechnicalTest.Domain.Extensions;

namespace WordDistanceTechnicalTest.Domain
{
    public interface IShortestPathCalculator
    {
        IReadOnlyCollection<string> GetShortestPathFromStartWordToEndWord(string startWord, string endWord, IReadOnlyCollection<string> dictionaryWords);
    }

    /// <summary>
    /// Calculates the shortest path between two words from a given set of words using a graph.
    /// </summary>
    public class ShortestPathCalculator : IShortestPathCalculator
    {
        public IReadOnlyCollection<string> GetShortestPathFromStartWordToEndWord(string startWord, string endWord,
            IReadOnlyCollection<string> dictionaryWords)
        {
            //TODO: Why have we got non-four letter words in here? Should these be stripped out?

            // Maybe an A* style approach with removing items from the dictionary where the common letters in the current and end word are kept, then all other items removed.

            var graph = GetGraphItems(dictionaryWords);
            var startWordGraphItem = graph.SingleOrDefault(e => e.Word == startWord);

            //TODO: I'm assuming here the start word must be in the graph.. Is that definitely the case?
            if (startWordGraphItem == null)
            {
                throw new ArgumentException();
            }

            return startWordGraphItem.WordsWithASingleChange.Select(e => FindPath(e, endWord, new List<GraphItem>() { startWordGraphItem }))
                .Where(e => e.All(a => a != null))
                .OrderBy(e => e.Count)
                .First()
                .Select(e => e.Word)
                .ToList();
        }

        private IReadOnlyCollection<GraphItem> FindPath(GraphItem currentWord, string endWord, IReadOnlyCollection<GraphItem> currentPath)
        {
            // If we've got the word then we can start to return 
            // This could just check that the word doesn't exist as a child graph item but I prefer this simpler termination condition.
            if (currentWord.Word == endWord)
            {
                return currentPath.Union(new[] { currentWord }).ToList();
            }
            else
            {
                // This isn't the most idiomatic code but this can be revisited later if I'm happy this is the right approach in the first place.
                var wordsNotAlreadyVisited = currentWord.WordsWithASingleChange.Where(e => !currentPath.Contains(e));
                return wordsNotAlreadyVisited.Select(e => FindPath(e, endWord, currentPath.Union(new [] { currentWord }).ToList()).ToList())
                    .Where(e => e != null)
                    .OrderBy(e => e.Count)
                    .FirstOrDefault();
            }
        }

        public IReadOnlyCollection<GraphItem> GetGraphItems(IReadOnlyCollection<string> dictionaryWords)
        {
            var allGraphItems = dictionaryWords.Select(e => new GraphItem(e)).ToList();

            foreach (var graphItem in allGraphItems)
            {
                // This should probably be an IEnumerable to allow for deferred execution? Depends on the use case of the app really.
                graphItem.WordsWithASingleChange = allGraphItems.Where(e => e.Word.GetWordDistanceTo(graphItem.Word) == 1).ToList();
            }

            return allGraphItems;
        }
    }

    public class GraphItem
    {
        public string Word { get; set; }
        /// <summary>
        /// TODO: This should really be lazy somehow (if we care about warmup performance).
        /// </summary>
        public IReadOnlyCollection<GraphItem> WordsWithASingleChange { get; set; }

        public GraphItem(string word)
        {
            Word = word;
        }
    }
}
