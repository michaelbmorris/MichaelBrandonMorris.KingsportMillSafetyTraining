using System.Web.Mvc;
using System.Web.Routing;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class RouteConfig.
    /// </summary>
    /// TODO Edit XML Comment Template for RouteConfig
    public class RouteConfig
    {
        /// <summary>
        ///     Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// TODO Edit XML Comment Template for RegisterRoutes
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}