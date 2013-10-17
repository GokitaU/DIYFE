using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFEWeb.Common.Extensions
{
    public static class StringExtensions
    {
        public static string TrimLastChar(this string str)
        {
            str = str.TrimEnd();
            return str.Substring(0, (str.Length - 1));
        }

        public static string RemoveInvalidChars(this string str)
        {
            str = str.Replace("|", "").Replace("&", "").Replace(" ", "");
            return str;
        }

        public static bool ContainsString(this string s1, string s2)
        {
            return s1.IndexOf(s2, StringComparison.OrdinalIgnoreCase) >= 0;
        }

    }
}