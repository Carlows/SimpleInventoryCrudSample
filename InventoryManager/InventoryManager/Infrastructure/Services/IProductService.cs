using InventoryManager.Models.Entities;
using InventoryManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManager.Infrastructure.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(ProductViewModel model);
        bool UpdateProduct(ProductViewModel model);
        bool DeleteProduct(int? id);
    }
}