using System;
using System.Web.Script.Serialization;

namespace MVC3Template
{
    public static class ExtMethods
    {
        //public static string ToJson(this Object obj)
        //{
        //    return new JavaScriptSerializer().Serialize(obj);
        //}

        //public static string Year(this DateTime? dt)
        //{
        //    return "1900";
        //}

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
    }
}