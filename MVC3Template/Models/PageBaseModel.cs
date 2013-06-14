using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC3Template.Models
{
    public class PageBaseModel
    {
        //SEO DATA
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }

        //PAGE VARS, MOSTLY USED FOR JAVASCRIPT
        public string ActiveTopNavLink { get; set; }
    }
}