using System.Web;
using System.Web.Optimization;

namespace DIYFEWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Clear();

            bundles.Add(new ScriptBundle("~/bundles/script/min").Include(
                        "~/Content/scripts/jquery-1.10.1.js",
                        "~/Content/scripts/Bootstrap/Twitter/js/bootstrap.js",
                        "~/Content/scripts/jquery.treeview.js",
                        "~/Content/scripts/Bootstrap/mediator.js",
                        "~/Content/scripts/Bootstrap/model.js",
                        "~/Content/scripts/Bootstrap/usefull.js",
                //"~/Content/scripts/Bootstrap/pager.js",
                        "~/Content/scripts/core.js",
                        "~/Content/scripts/App/admin.js",
                        "~/Content/scripts/App/contact.js",
                        "~/Content/scripts/App/comment.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/script/min").Include(
            //            "~/Content/scripts/jquery-1.10.1.js",        
            //            "~/Content/scripts/Bootstrap/Twitter/js/bootstrap.js",       
            //            "~/Content/scripts/jquery.treeview.js",
            //            "~/Content/scripts/Bootstrap/mediator.js",
            //            "~/Content/scripts/Bootstrap/model.js",
            //            "~/Content/scripts/Bootstrap/usefull.js",
            //            //"~/Content/scripts/Bootstrap/pager.js",
            //            "~/Content/scripts/core.js",
            //            "~/Content/scripts/App/admin.js",
            //            "~/Content/scripts/App/contact.js",
            //            "~/Content/scripts/App/comment.js"
            //            ));


            bundles.Add(new StyleBundle("~/bundles/css/min").Include(
                //"~/Content/min/DIYFE_min.css"
                "~/Content/scripts/Bootstrap/Twitter/css/bootstrap.css",
                "~/Content/site.css",
                "~/Content/comments.css"
                //        "~/Content/scripts/Bootstrap/Twitter/css/bootstrap-responsive.css",
                ////"~/Content/scripts/themes.css",));
                //        "~/Content/site.css",
                //        "~/Content/jquery.treeview.css",
                //        "~/Content/comments.css"
                        ));


            //bundles.Add(new ScriptBundle("~/Content/min").Include(
            //            "~/Content/Scripts/jquery-{version}.js",
            //            "~/Content/Scripts/jquery{addOn}.js",
            //            "~/Content/Scripts/jquery{addOn}.js"));


            //bundles.Add(new StyleBundle("~/Content/min").Include(
            //            "~/Content/scripts/Bootstrap/Twitter/bootstrap-responsive.css",
            //            //"~/Content/scripts/themes.css",));
            //            "~/Content/site.css",
            //            "~/Content/jquery.treeview.css",
            //            "~/Content/comments.css"
            //            ));


            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}