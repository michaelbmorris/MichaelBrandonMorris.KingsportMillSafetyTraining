using System.Web.Mvc;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class FilterConfig.
    /// </summary>
    /// TODO Edit XML Comment Template for FilterConfig
    public class FilterConfig
    {
        /// <summary>
        ///     Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// TODO Edit XML Comment Template for RegisterGlobalFilters
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}