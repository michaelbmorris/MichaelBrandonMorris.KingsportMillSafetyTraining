using System.Web.Optimization;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Class BundleConfig.
    /// </summary>
    /// TODO Edit XML Comment Template for BundleConfig
    public class BundleConfig
    {
        /// <summary>
        ///     Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        /// TODO Edit XML Comment Template for RegisterBundles
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*",
                    "~/Scripts/MvcFoolProofJQueryValidation.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/respond.js",
                    "~/Scripts/moment.js",
                    "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    "~/Content/reset.css",
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-datetimepicker.css",
                    "~/Content/site.css"));
        }
    }
}