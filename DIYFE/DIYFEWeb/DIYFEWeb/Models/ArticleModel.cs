using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFELib;

namespace DIYFEWeb.Models
{
    public class ArticleModel
    {
        public Article ArticleContent = new Article();

        //BREAD CRUMB DATA
        public List<CustomHtmlLink> CrumbLinkList { get; set; }
        //RELATED TREEVIEW DATA
        public List<CustomHtmlLink> RelatedTreeView { get; set; }
        //SIDE CONTENT
        public List<CustomHtmlLink> MostViewed { get; set; }
        //COMMENTS
        public List<ArticleComment> Comments { get; set; }
        //DATA LIST
        public DIYFELib.ArticleList ArticleList { get; set; }
    }
}