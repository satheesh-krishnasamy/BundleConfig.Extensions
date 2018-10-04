using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace BundleConfigExtensions
{
    public static class MVCBundleToConfigExtensions
    {
        public static string GetBundleCollectionAsJSonString(this BundleCollection mvcBundleConfig)
        {
            var bundles = mvcBundleConfig.GetRegisteredBundles();
            foreach(var bundle in bundles)
            {
                //bundle.
            }

            return string.Empty;
        }
    }
}
