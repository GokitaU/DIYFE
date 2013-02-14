using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DIYFE.Web
{
    public static class HtmlHelpers
    {
        private const string Nbsp = "&nbsp;";

        public static MvcHtmlString NbspIfEmpty(this HtmlHelper helper, string value)
        {
            return new MvcHtmlString(string.IsNullOrEmpty(value) ? Nbsp : value);
        }

        public static MvcHtmlString NBAMainNav()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<div class=\"well sidebar-nav\">");
            sb.AppendLine("<ul class=\"nav nav-list\">");
              sb.AppendLine("<li class=\"nav-header\">NBA Nav</li>");
              sb.AppendLine("<li><a href=\"http://diyfe.org/sports-betting/nba/BetList?seasonId=12\">Bet List Details</a></li>");
              sb.AppendLine("<li><a href=\"http://diyfe.org/sports-betting/nba/excel?seasonId=12\">Excel View</a></li>");
              sb.AppendLine("<li><a href=\"http://diyfe.org/sports-betting/nba/bydate?seasonId=12\">Running View</a></li>");
              sb.AppendLine("<li><a href=\"http://diyfe.org/sports-betting/nba/TeamSchedule?teamId=31&seasonId=12\">Team Schedules</a></li>");
              sb.AppendLine("<li class=\"nav-header\">Old Link List</li>");
              sb.AppendLine("<li><a id=\"clearScrap\" href=\"#\">Clear Scraping</a></li>");
              sb.AppendLine("<li><a href=\"#\">Link</a></li>");
              sb.AppendLine("<li><a id=\"runSpreadCrapingLnk\" href=\"#\">Run Spread Scraping</a></li>");
              sb.AppendLine("<li><a href=\"#\">Link</a></li>");
              sb.AppendLine("<li><a href=\"#\">Link</a></li>");
            sb.AppendLine("</ul>");
          sb.AppendLine("</div>");

          return MvcHtmlString.Create(sb.ToString());
        }
    }
}