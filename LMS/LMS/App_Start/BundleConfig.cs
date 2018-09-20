using System.Web;
using System.Web.Optimization;

namespace LMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/Scripts/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resources/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Resources/Scripts/bootstrap/bootstrap.js",
                      "~/Resources/Scripts/bootstrap-datetimepicker/moment.js",
                      "~/Resources/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js",
                      "~/Resources/Scripts/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                    "~/Resources/Scripts/Site.js",
                    "~/Resources/Scripts/Validation.js",
                    "~/Resources/Scripts/ajax/AjaxHttpSender.js",
                    "~/Resources/Scripts/css-element-queries/ElementQueries.js",
                    "~/Resources/Scripts/css-element-queries/ResizeSensor.js"));

            bundles.Add(new ScriptBundle("~/bundles/dialogs").Include(
                "~/Resources/Scripts/dialogs/DialogTypeEnum.js",
                "~/Resources/Scripts/dialogs/DialogUtility.js",
                "~/Resources/Scripts/dialogs/BaseDialog.js",
                "~/Resources/Scripts/dialogs/DialogFactory.js"));

            bundles.Add(new ScriptBundle("~/bundles/dialogs/chat")
                .IncludeDirectory("~/Resources/Scripts/dialogs/Chat", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/dialogs/user")
                .IncludeDirectory("~/Resources/Scripts/dialogs/User", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/grid")
                .IncludeDirectory("~/Resources/Scripts/lms-grid", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/ribbon")
                .IncludeDirectory("~/Resources/Scripts/lms-ribbon", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/select")
               .IncludeDirectory("~/Resources/Scripts/select", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/signalR")
               .IncludeDirectory("~/Resources/Scripts/signalR", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/toastr")
               .IncludeDirectory("~/Resources/Scripts/toastr", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                    "~/Resources/Scripts/css-element-queries/ElementQueries.js",
                    "~/Resources/Scripts/ResizeSensor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Resources/Styles/bootstrap/bootstrap.css",
                      "~/Resources/Styles/Site.css",
                      "~/Resources/Styles/lms-grid/LMSGrid.css",
                      "~/Resources/Styles/lms-ribbon/LMSRibbon.css"));
        }
    }
}
