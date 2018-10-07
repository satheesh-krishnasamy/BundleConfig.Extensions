using System;

namespace BundleConfigExtensions.Model
{
    public class DuplicateBundleException: Exception
    {
        public DuplicateBundleException(string message): base(message)
        {

        }
    }
}
