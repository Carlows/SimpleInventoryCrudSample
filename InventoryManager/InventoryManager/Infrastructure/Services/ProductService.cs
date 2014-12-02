using InventoryManager.Models;
using InventoryManager.Models.Entities;
using InventoryManager.Models.Repositories;
using InventoryManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace InventoryManager.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _repo.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _repo.GetById(id);
        }

        public Product AddProduct(ProductViewModel product)
        {
            Mapper.CreateMap<ProductViewModel, Product>();
            Product pModel = Mapper.Map<ProductViewModel, Product>(product);

            var result = _repo.Add(pModel);

            if (result == null)
                return null;
            else
            {
                Product p = _repo.GetById((int)result);
                return p;
            }
        }


        public bool UpdateProduct(ProductViewModel product)
        {
            Mapper.CreateMap<ProductViewModel, Product>();
            Product pModel = Mapper.Map<ProductViewModel, Product>(product);

            var result = _repo.Update(pModel);

            if (result)
                return true;
            else
                return false;
        }

        public bool DeleteProduct(int? id)
        {
            bool result = _repo.Delete(id);

            if (result)
                return true;
            else
                return false;
        }

    }
}