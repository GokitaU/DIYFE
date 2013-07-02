﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class DIYFEEntities : DbContext
    {
        public DIYFEEntities()
            : base("name=DIYFEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleResource> ArticleResources { get; set; }
        public DbSet<ArticleResourceType> ArticleResourceTypes { get; set; }
        public DbSet<ArticleStatus> ArticleStatus { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Article> Articles { get; set; }
    
        public virtual int sp_LoadArticle(string articleName, Nullable<int> articleId)
        {
            var articleNameParameter = articleName != null ?
                new ObjectParameter("articleName", articleName) :
                new ObjectParameter("articleName", typeof(string));
    
            var articleIdParameter = articleId.HasValue ?
                new ObjectParameter("articleId", articleId) :
                new ObjectParameter("articleId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_LoadArticle", articleNameParameter, articleIdParameter);
        }
    
        public virtual int sp_LoadPostList(Nullable<int> categoryRowId)
        {
            var categoryRowIdParameter = categoryRowId.HasValue ?
                new ObjectParameter("CategoryRowId", categoryRowId) :
                new ObjectParameter("CategoryRowId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_LoadPostList", categoryRowIdParameter);
        }
    
        public virtual int sp_SeasonGames(Nullable<int> seasonId)
        {
            var seasonIdParameter = seasonId.HasValue ?
                new ObjectParameter("seasonId", seasonId) :
                new ObjectParameter("seasonId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_SeasonGames", seasonIdParameter);
        }
    }
}
