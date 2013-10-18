using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFE.EF;
using PagedList;

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
        //public List<Article> ArticleList { get; set; }
        //DATA LIST
        public IEnumerable<Article> ArticleList { get; set; }
        //PAGED LIST
        public IPagedList<Article> PagedArticle { get; set; }
    }
}