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
    
    public partial class DailyRecipe
    {
        public string Name { get; set; }
        public string RecipeName { get; set; }
        public string Instructions { get; set; }
        public int UseAmount { get; set; }
        public decimal Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string IngredientName { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string Discription { get; set; }
        public int RecipeDayId { get; set; }
        public int DayId { get; set; }
    }
}