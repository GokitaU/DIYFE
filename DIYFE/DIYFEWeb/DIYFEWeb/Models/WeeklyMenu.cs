using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFE.EF;

namespace DIYFEWeb.Models
{
    public class WeeklyMenu
    {
        public List<IngredientShopping> ShoppingList = new List<IngredientShopping>();
        public List<DailyRecipe> DailyRecipes = new List<DailyRecipe>();
        public List<SeasonalRecipe> SeasonalRecipes = new List<SeasonalRecipe>();
    }
}