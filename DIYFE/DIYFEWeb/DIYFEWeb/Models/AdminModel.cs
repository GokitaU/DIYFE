﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DIYFE.EF;

namespace DIYFEWeb.Models
{
    public class AdminModel
    {
        public List<Category> Categories = new List<Category>();
        public Article MainArticle = new Article();
    }
}