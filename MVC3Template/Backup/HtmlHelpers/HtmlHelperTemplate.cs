using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3Template.HtmlHelpers
{
    public static class HtmlHelperTemplate
    {
        private const string Nbsp = "&nbsp;";

        public static MvcHtmlString NbspIfEmpty(this HtmlHelper helper, string value)
        {
            return new MvcHtmlString(string.IsNullOrEmpty(value) ? Nbsp : value);
        }

    }
}