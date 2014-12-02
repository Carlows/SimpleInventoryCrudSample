using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManager.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int InStock { get; set; }
    }
}