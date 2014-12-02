using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManager.Models;
using InventoryManager.Infrastructure.Services;
using InventoryManager.Models.ViewModels;
using System.Dynamic;
using InventoryManager.Models.Entities;
using AutoMapper;

namespace InventoryManager.Controllers
{
    public class HomeController : Controller
    {
        private IProductService _productService;

        public HomeController(IProductService pService)
        {
            this._productService = pService;
        }

        public ActionResult Index()
        {
            var model = _productService.GetAllProducts();

            return View(model);
        }

        public ActionResult UpdateProduct(int? productid)
        {
            if (productid == null)
            {
                TempData["error"] = "No se pudo encontrar el producto";
                return RedirectToAction("Index");
            }

            var product = _productService.GetProductById((int)productid);

            Mapper.CreateMap<Product, ProductViewModel>();
            ProductViewModel model = Mapper.Map<Product, ProductViewModel>(product);

            if(model == null)
            {
                TempData["error"] = "No se pudo encontrar el producto";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = _productService.UpdateProduct(model);

                if(result)
                {
                    TempData["error"] = "El producto se actualizo correctamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Hubo un error al actualizar el producto");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model)
        {
            var jsonResult = new { Success = false };

            if(ModelState.IsValid)
            {
                Product product = _productService.AddProduct(model);

                if (product == null)
                {
                    return Json(jsonResult);
                }
                else
                {
                    var productResult = new { Success = true, Product = product };
                    return Json(productResult);
                }               
            }

            return Json(jsonResult);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int? id)
        {
            var jsonResult = new { Success = false };

            if(id != null)
            {
                bool result = _productService.DeleteProduct(id);

                if(result)
                {
                    jsonResult = new { Success = true };
                }

                return Json(jsonResult);
            }

            return Json(jsonResult);
        }
    }
}