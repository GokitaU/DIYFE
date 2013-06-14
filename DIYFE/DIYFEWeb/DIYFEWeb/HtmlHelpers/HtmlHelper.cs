using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DIYFEWeb
{
    public static class HtmlHelpers
    {
        private const string Nbsp = "&nbsp;";

        public static HtmlString NbspIfEmpty(this HtmlHelper helper, string value)
        {
            return new HtmlString(string.IsNullOrEmpty(value) ? Nbsp : value);
        }
    }
}