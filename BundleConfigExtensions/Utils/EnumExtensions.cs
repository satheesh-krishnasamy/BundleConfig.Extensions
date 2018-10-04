using System;
using System.Text;

namespace BundleConfigExtensions.Utils
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberNames<T>()
        {
            var resultHolder = new StringBuilder();
            var enumMembers = Enum.GetNames(typeof(T));
            foreach (var member in enumMembers)
                resultHolder.Append($"{member}, ");

            // remove the comma and the space at the end of the string.
            if (resultHolder.Length > 2)
                return resultHolder.Remove(resultHolder.Length - 2, 2).ToString();

            return resultHolder.ToString();
        }
    }
}
