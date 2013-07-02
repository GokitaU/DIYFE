using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFE.EF;

namespace DIYFEWeb.Models
{
    public class ArticleListModel
    {
        //BREAD CRUMB DATA
        public List<CustomHtmlLink> CrumbLinkList { get; set; }
        //RELATED TREEVIEW DATA
        public List<CustomHtmlLink> RelatedTreeView { get; set; }
        //SIDE CONTENT
        public List<CustomHtmlLink> MostViewed { get; set; }
        //COMMENTS
        public List<ArticleComment> Comments { get; set; }
        //DATA LIST
        public List<Article> ProjectList { get; set; }
        //DATA LIST
        public List<Article> ArticleList { get; set; }
    }
}