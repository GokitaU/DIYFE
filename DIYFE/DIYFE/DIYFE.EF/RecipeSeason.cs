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
    
    public partial class RecipeSeason
    {
        public int RecipeSeasonId { get; set; }
        public int SeasonId { get; set; }
        public int RecipeId { get; set; }
    
        public virtual Recipe Recipe { get; set; }
        public virtual Season Season { get; set; }
    }
}
