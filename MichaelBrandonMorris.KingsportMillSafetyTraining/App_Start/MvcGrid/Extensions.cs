using System;
using System.Collections.Generic;
using System.Linq;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.MvcGrid
{
    internal static class Extensions
    {
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
    }
}