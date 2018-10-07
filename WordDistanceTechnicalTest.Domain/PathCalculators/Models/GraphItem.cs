using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WordDistanceTechnicalTest.Domain.Extensions;

namespace WordDistanceTechnicalTest.Domain.PathCalculators.Models
{
    [DebuggerDisplay("Word = '{Word}'")]
    internal class GraphItem
    {
        public string Word { get; }
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

        public void SetWordsWithASingleChange(List<GraphItem> allGraphItems)
        {
            // I did have this as eager execution but there's no need to generate the entire graph upfront.
            WordsWithASingleChange = allGraphItems.Where(e => e.Word.HasOnlySingleLetterDifference(Word));
        }
    }
}