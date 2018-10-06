using System.Collections.Generic;
using System.Linq;
using WordDistanceTechnicalTest.Domain.PathCalculators.Models;

namespace WordDistanceTechnicalTest.Domain.Extensions
{
    internal static class QueueExtensions
    {
        /// <summary>
        /// Removes any graph paths which are equal to or greater than a specified length.
        /// </summary>
        internal static Queue<GraphPath> RemovePathsWithALengthEqualOrGreaterThan(this Queue<GraphPath> paths, int length)
        {
            return new Queue<GraphPath>(paths.Where(e => e.CurrentPath.Count < length));
        }
    }
}
