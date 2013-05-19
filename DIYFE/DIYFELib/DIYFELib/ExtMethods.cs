using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace DIYFELib
{
    public static class ExtMethods
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

        public static DateTime GetDateSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDateTime(colIndex);
            else
                return DateTime.MinValue;
        }

        public static bool HasPlayed(this DateTime gameDate)
        {
            if (gameDate.Date < DateTime.Now.Date)
            { return true; }
            else
            { return false; }
        }

        public static string GetStringSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return string.Empty;
        }

        public static int GetInt32Safe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return 0;
        }

        public static decimal GetDecimalSafe(this SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetDecimal(colIndex);
            else
                return 0;
        }
    }
}

