using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFELib;

namespace DIYFEWeb.Models
{
    public class ProjectListModel
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
        public List<DIYFE.EF.Article> ProjectList { get; set; }
        //DATA LIST
        public List<DIYFE.EF.Article> ArticleList { get; set; }
    }
}