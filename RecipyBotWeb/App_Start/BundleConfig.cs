using System.Web;
using System.Web.Optimization;

namespace RecipyBotWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JAVASCRIPT
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Vendor/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Vendor/bootstrap.js",
                      "~/Scripts/Vendor/respond.js",
                      "~/Scripts/Vendor/typed.min.js"));
            #endregion

            #region CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Vendor/bootstrap.css",
                      "~/Content/main.css"));
            #endregion
        }
    }
}
