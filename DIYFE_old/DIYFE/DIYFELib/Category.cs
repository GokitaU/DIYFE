using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIYFELib
{
    public class Category
    {
        private int _categoryRowId { get; set; }
        private int _categoryId { get; set; }
        private int _secondLevCategoryId { get; set; }
        private int _thirdLevCategoryId { get; set; }

        private string _categoryName {get;set;}
        private string _categoryUrl { get; set; }
        private string _secondLevCategoryName { get; set; }
        private string _secondLevCategoryUrl { get; set; }
        private string _thirdLevCategoryName { get; set; }
        private string _thirdLevCategoryUrl { get; set; }


        public int CategoryRowId,
                   CategoryId, 
                   SecondLevCategoryId, 
                   ThirdLevCategoryId;
        public string CategoryName, CategoryUrl,
                      SecondLevCategoryName, SecondLevCategoryUrl,
                      ThirdLevCategoryName, ThirdLevCategoryUrl;

        


    }
}
