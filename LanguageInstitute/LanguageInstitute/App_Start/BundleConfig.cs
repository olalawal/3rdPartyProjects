using System.Web;
using System.Web.Optimization;

namespace LanguageInstitute
{
    public class BundleConfig
    {

    
  


        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundlesNew(bundles);
            RegisterJavascriptBundlesNew(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
                            .Include("~/Content/site.css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/carousel.css")
                            .Include("~/scripts/CurrentTemplate/fancybox/jquery.fancybox-1.3.4.css")
                          .Include("~/Images")
                            );
        }

    //    <link media="screen" charset="utf-8" rel="stylesheet" href="~/Content/css/base.css" />
    //<link media="screen" charset="utf-8" rel="stylesheet"  href="~/Content/css/skeleton.css" />
    //<link media="screen" charset="utf-8" rel="stylesheet"  href="~/Content/css/layout.css" />
    //<link media="screen" charset="utf-8" rel="stylesheet"  href="~/Content/css/child.css" />
    //<link rel="stylesheet"  href="~/Content/css/animate.min.css" type="text/css" media="screen" charset="utf-8" />
    //<link rel="stylesheet"  href="~/Content/css/jquery.onebyone.css" type="text/css" media="screen" charset="utf-8" />
    //<link rel="stylesheet"  href="~/Content/css/prettyPhoto.css" type="text/css" media="screen" charset="utf-8" />


        
        private static void RegisterStyleBundlesNew(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
          //.Include("~/Content/bootstrap.css")
                          
                            .Include("~/Content/reset.css")
                            .Include("~/Content/css/base.css")
                            .Include( "~/Content/bootstrap-notify.css")
                            .Include("~/Content/css/skeleton.css")
                            .Include("~/Content/css/layout.css")
                            .Include("~/Content/css/child.css")

                            .Include("~/Content/css/animate.min.css")
                            .Include("~/Content/css/jquery.onebyone.css")
                            .Include("~/Content/css/prettyPhoto.css")

                            .Include("~/Content/jquery-ui.css")
                            .Include("~/Content/JqueryTE/jquery-te-{version}.css")                         
                          .Include("~/Images")
                           );

            bundles.Add(new StyleBundle("~/fancybox")
                            .Include("~/Content/jquery.fancybox.css")
                            .Include("~/Content/jquery.fancybox-buttons.css")
                            .Include("~/Content/jquery.fancybox-thumbs.css")
                           );
        }

        public static void RegisterJavascriptBundlesNew(BundleCollection bundles)
        {



            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                          "~/Scripts/jquery.easing.1.3.js",

                          "~/Scripts/jquery.carousel.js",
                          "~/Scripts/jquery.color.animation.js",
                          "~/Scripts/jquery.prettyPhoto.js",
                          "~/Scripts/jquery.onebyone.min.js",
                          "~/Scripts/jquery.touchwipe.min.js",

                         "~/Scripts/jquery-migrate-{version}.js",
                         "~/Scripts/JqueryTE/jquery-te-{version}.js",
                          "~/Scripts/jquery.blockUI.js"
                         ));
            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                       "~/Scripts/form.js", 
                       "~/Scripts/Statistical.Alert.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

                   bundles.Add(new ScriptBundle("~/bundles/HTM5SHIV").Include(
                        "~/Scripts/html5shiv.js",
                         "~/Scripts/html5shiv-printshiv.js"
                        ));

                   bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js", "~/Scripts/bootstrap-notify.js"));
                          
                  bundles.Add(new ScriptBundle("~/bundles/base").Include(                       
                        "~/Scripts/ default.js",                  
                          "~/Scripts/jquery.mousewheel-3.0.6.pack.js",
                         "~/Scripts/jquery.fancybox.js",
                         "~/Scripts/jquery.fancybox-buttons.js",
                         "~/Scripts/jquery.fancybox-media.js",
                         "~/Scripts/jquery.fancybox-thumbs.js"                        
                    ));
        }


        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


              bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                       "~/Scripts/bootstrap.js"));
             

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }

          

    }
}