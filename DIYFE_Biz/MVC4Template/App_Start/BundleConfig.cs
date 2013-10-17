using System.Web;
using System.Web.Optimization;

namespace DIYFEWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
           
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Content/Frameworks/js/bootstrap.min.js",
                        "~/Scripts/usefull.js",
                        "~/Scripts/mediator.js",
                        "~/Scripts/app.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/Frameworks/css/bootstrap.min.css"));


        }
    }
}