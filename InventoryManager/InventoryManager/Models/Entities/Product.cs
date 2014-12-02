﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManager.Models.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
    }
}