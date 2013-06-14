using System;

namespace HW.Gorilla.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string DefaultShortDateTimeFormat
        {
            get
            {
                return "MM/dd/yyyy";
            }
        }

        public static string DefaultUniversalDateTimeFormat
        {
            get
            {
                return "U";
            }
        }

        public static string DefaultGeneralDateTimeFormat
        {
            get
            {
                return "g";
            }
        }

        public static string ToShortDefaultFormat(this DateTime d)
        {
            return d.ToStringInCulture(DefaultShortDateTimeFormat);
        }
        public static string ToUniversalDefaultFormat(this DateTime d)
        {
            return d.ToStringInCulture(DefaultUniversalDateTimeFormat);
        }
        public static string ToGeneralDefaultFormat(this DateTime d)
        {
            return d.ToStringInCulture(DefaultGeneralDateTimeFormat);
        }
        public static string ToLongYear(this DateTime d)
        {
            return d.ToStringInCulture("yyyy");
        }
        public static string ToStringInCulture(this DateTime d, string format)
        {
            return d.ToString(format, System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
