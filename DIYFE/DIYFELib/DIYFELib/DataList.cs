using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIYFELib
{
    public class DataList
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public List<DataListItem> ListItems { get; set; }
    }
}