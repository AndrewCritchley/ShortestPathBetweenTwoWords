using System.Collections.Generic;
using System.Linq;

namespace WordDistanceTechnicalTest.Domain.PathCalculators.Models
{
    /// <summary>
    /// Represents a path through the graph from a given start word to an end word.
    /// </summary>
    internal class GraphPath
    {
        public IReadOnlyCollection<GraphItem> CurrentPath { get; }

        public GraphPath(IReadOnlyCollection<GraphItem> currentPath)
        {
            CurrentPath = currentPath;
        }

        public GraphPath(GraphPath currentGraphPath)
        {
            CurrentPath = currentGraphPath.CurrentPath;
        }

        public static GraphPath operator +(GraphPath p1, GraphItem p2)
        {
            var newPath = p1.CurrentPath.Union(new[] { p2 }).ToList();
            return new GraphPath(newPath);
        }

        /// <summary>
        /// Creates any paths possible from the child nodes of the last item that have not been visited yet as part of this path.
        /// </summary>
        public IReadOnlyCollection<GraphPath> CreateUnvisitedPaths()
        {
            var siblingWordsNotAlreadyVisited = CurrentPath
                .Last()
                .WordsWithASingleChange
                .Except(CurrentPath);

            return siblingWordsNotAlreadyVisited.Select(e => new GraphPath(this + e)).ToList();
        }

        public IReadOnlyCollection<string> AsStrings()
        {
            return CurrentPath
                .Select(e => e.Word)
                .ToList();
        }
    }
}
