using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class MvcApplication.
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    /// TODO Edit XML Comment Template for MvcApplication
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        ///     Applications the start.
        /// </summary>
        /// TODO Edit XML Comment Template for Application_Start
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}