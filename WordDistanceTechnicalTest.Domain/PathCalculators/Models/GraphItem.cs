using System.Collections.Generic;
using System.Diagnostics;

namespace WordDistanceTechnicalTest.Domain.PathCalculators.Models
{
    [DebuggerDisplay("Word = '{Word}'")]
    internal class GraphItem
    {
        public string Word { get; set; }
        public IEnumerable<GraphItem> WordsWithASingleChange { get; set; }

        public GraphItem(string word)
        {
            Word = word;
        }

        public static GraphPath operator +(GraphItem p1, GraphItem p2)
        {
            var newPath = new[] { p1, p2 };
            return new GraphPath(newPath);
        }
    }
}
