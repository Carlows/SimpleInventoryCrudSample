using InventoryManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryManager.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _db;

        public ProductRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.ToList();
        }

        public int? Add(Product product)
        {
            try
            {
                _db.Products.Add(product);
                _db.SaveChanges();

                return product.ProductID;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public Product GetById(int id)
        {
            try
            {
                Product product = _db.Products.Find(id);

                return product;
            }
            catch(Exception e)
            {
                return null;
            }
        }


        public bool Delete(int? id)
        {
            try
            {
                Product product = _db.Products.Find(id);
                _db.Products.Remove(product);
                _db.SaveChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


    }
}