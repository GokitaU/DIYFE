using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

using DIYFELib;

namespace DIYFEWeb.Models
{
    public class PageBaseModel
    {
        //BASE SEO DATA AND SIMPLE CONTENT
        public Article ArticleContent = new Article();

        //PAGE VARS, MOSTLY USED FOR JAVASCRIPT
        public string ActiveTopNavLink { get; set; }


    }
}