using System.Web.Optimization;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining
{
    /// <summary>
    ///     Manages the <see cref="BundleCollection"/> for the application.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        ///     Adds the application's <see cref="Bundle"/>s to the 
        ///     <see cref="BundleCollection"/>.
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.validate*"));

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