using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIYFELib
{
    public class CustomHtmlLink
    {
        public string LinkText { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }
        public int Views { get; set; }
        public string GenerateHtml
        {
            get
            {
                return "<a href=\"" + this.Href.ToLower() + "\" title=\"" + this.Title + "\">" + this.LinkText + "</a>";
            }
        }
        public List<CustomHtmlLink> SubLinks { get; set; }
    }
}
