using InventoryManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManager.Models.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        int? Add(Product product);
        bool Update(Product product);
        bool Delete(int? id);
    }
}