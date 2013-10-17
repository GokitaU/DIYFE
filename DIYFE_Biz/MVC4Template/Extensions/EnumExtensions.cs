
using System;
using System.Reflection;

namespace DIYFEWeb.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            FieldInfo fieldInfo = type.GetField(value.ToString());

            return "";
        }
    }
}
