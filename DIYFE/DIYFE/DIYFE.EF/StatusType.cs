//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DIYFE.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusType
    {
        public StatusType()
        {
            this.ArticleStatus = new HashSet<ArticleStatus>();
        }
    
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    
        public virtual ICollection<ArticleStatus> ArticleStatus { get; set; }
    }
}
