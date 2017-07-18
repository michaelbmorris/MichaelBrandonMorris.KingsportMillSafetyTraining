using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.Extensions.CollectionExtensions;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    /// <summary>
    ///     Class Extensions.
    /// </summary>
    /// TODO Edit XML Comment Template for Extensions
    internal static class Extensions
    {
        /// <summary>
        ///     Orders the by.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     sortDirection
        ///     - null
        /// </exception>
        /// TODO Edit XML Comment Template for OrderBy`1
        internal static IList<T> OrderBy<T>(
            this IEnumerable<T> enumerable,
            Func<T, object> sortBy,
            SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Unspecified: return enumerable.ToList();
                case SortDirection.Asc:
                    return enumerable.OrderBy(sortBy).ToList();
                case SortDirection.Dsc:
                    return enumerable.OrderByDescending(sortBy).ToList();
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(sortDirection),
                        sortDirection,
                        null);
            }
        }

        internal static bool ContainsFilter(this string s, string filter)
        {
            return filter.IsNullOrWhiteSpace() || s.ContainsIgnoreCase(filter);
        }

        internal static IList<T> OrderByWhere<T>(
            this IEnumerable<T> enumerable,
            Func<T, object> orderBy,
            Func<T, bool> where,
            SortDirection direction)
        {
            switch (direction)
            {
                case SortDirection.Unspecified:
                    return enumerable.Where(where).ToList();
                case SortDirection.Asc:
                    return enumerable.OrderByWhere(orderBy, where);
                case SortDirection.Dsc:
                    return enumerable.OrderByDescendingWhere(orderBy, where);
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(direction),
                        direction,
                        null);
            }
        }
    }
}