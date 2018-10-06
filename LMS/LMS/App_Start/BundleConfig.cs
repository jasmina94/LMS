using System.Web;
using System.Web.Optimization;

namespace LMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterCustomScripts(bundles);
            RegisterStyles(bundles);        
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Resources/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Resources/Scripts/bootstrap/bootstrap.js",
                      "~/Resources/Scripts/bootstrap-datetimepicker/moment.js",
                      "~/Resources/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js",
                      "~/Resources/Scripts/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                    "~/Resources/Scripts/jquery/jquery-ui-1.12.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/select").Include(
                "~/Resources/Scripts/select/select2.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").IncludeDirectory(
                "~/Resources/Scripts/signalR", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/toastr").IncludeDirectory(
                "~/Resources/Scripts/toastr", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/css").Include(
                   "~/Resources/Scripts/css-element-queries/ElementQueries.js",
                   "~/Resources/Scripts/ResizeSensor.js"));
        }

        private static void RegisterCustomScripts(BundleCollection bundles)
        {
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

            bundles.Add(new ScriptBundle("~/bundles/dialogs/category")
                .IncludeDirectory("~/Resources/Scripts/dialogs/Category", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/dialogs/language")
               .IncludeDirectory("~/Resources/Scripts/dialogs/Language", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/dialogs/book")
               .IncludeDirectory("~/Resources/Scripts/dialogs/Book", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/grid")
                .IncludeDirectory("~/Resources/Scripts/lms-grid", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/ribbon")
                .IncludeDirectory("~/Resources/Scripts/lms-ribbon", "*.js", true));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                      "~/Resources/Styles/bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/jqueryUI/css").IncludeDirectory(
                    "~/Resources/Styles/jqueryUI", "*.css", true));

            bundles.Add(new StyleBundle("~/select/css").Include(
                   "~/Resources/Styles/select/select2.css",
                   "~/Resources/Styles/select/select2-bootstrap.css"));

            bundles.Add(new StyleBundle("~/toastr/css").Include(
                   "~/Resources/Styles/toastr/toastr.css"));

            bundles.Add(new StyleBundle("~/custom/css").Include(
                      "~/Resources/Styles/Site.css",
                      "~/Resources/Styles/lms-grid/LMSGrid.css",
                      "~/Resources/Styles/lms-ribbon/LMSRibbon.css"));
        }
    }
}
