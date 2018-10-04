using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;
using BundleConfigExtensions.Model;
using Newtonsoft.Json;

namespace Sample.MVC.App
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            #region JS bundles
            IList<BundleDefinitionJson> bundleConfigJsonList = new List<BundleDefinitionJson>();
            bundleConfigJsonList.Add(new BundleDefinitionJson()
            {
                BundleContentType = BundleConfigExtensions.Model.BundleType.Javascript,
                BundleVirtualPathOrName = "~/bundles/jquery",
                FilesVirtualPath = new List<BundleFileDetails>()
            {
                new BundleFileDetails(){InputFilePath =  "~/Scripts/jquery-{version}.js", OrderInTheBundle =1 }
            }
            });

            bundleConfigJsonList.Add(new BundleDefinitionJson()
            {
                BundleContentType = BundleConfigExtensions.Model.BundleType.Javascript,
                BundleVirtualPathOrName = "~/bundles/jqueryval",
                FilesVirtualPath = new List<BundleFileDetails>()
            {
                new BundleFileDetails(){InputFilePath =  "~/Scripts/jquery.validate*", OrderInTheBundle =1 }
            }
            });

            bundleConfigJsonList.Add(new BundleDefinitionJson()
            {
                BundleContentType = BundleConfigExtensions.Model.BundleType.Javascript,
                BundleVirtualPathOrName = "~/bundles/modernizr",
                FilesVirtualPath = new List<BundleFileDetails>()
            {
                new BundleFileDetails(){InputFilePath =  "~/Scripts/modernizr-*", OrderInTheBundle =1 }
            }
            });

            bundleConfigJsonList.Add(new BundleDefinitionJson()
            {
                BundleContentType = BundleConfigExtensions.Model.BundleType.Javascript,
                BundleVirtualPathOrName = "~/bundles/bootstrap",
                FilesVirtualPath = new List<BundleFileDetails>()
            {
                new BundleFileDetails(){InputFilePath =  "~/Scripts/bootstrap.js", OrderInTheBundle =1 }
            }
            });

            var stringJSBundles = JsonConvert.SerializeObject(bundleConfigJsonList);

            #endregion JS bundles


            #region CSS bundles
            IList<BundleDefinitionJson> cssBundleConfigJsonList = new List<BundleDefinitionJson>();
            cssBundleConfigJsonList.Add(new BundleDefinitionJson()
            {
                BundleContentType = BundleConfigExtensions.Model.BundleType.CascadedStyleSheet,
                BundleVirtualPathOrName = "~/Content/css",
                FilesVirtualPath = new List<BundleFileDetails>()
            {
                new BundleFileDetails(){InputFilePath =  "~/Content/bootstrap.css", OrderInTheBundle =1 },
                new BundleFileDetails(){InputFilePath =  "~/Content/site.css", OrderInTheBundle = 2 }
            }
            });

            var stringCssBundles = JsonConvert.SerializeObject(cssBundleConfigJsonList);

            #endregion CSS bundles
        }
    }
}
