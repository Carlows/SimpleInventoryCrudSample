using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using InventoryManager.Models.Entities;
using InventoryManager.Infrastructure.Services;
using Moq;
using InventoryManager.Controllers;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using InventoryManager.Models.ViewModels;
using System.Web.Script.Serialization;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>
            {
                new Product{ ProductID = 1, Name = "Product 1", Type = "Type 1", InStock = 10, Price = 200 },
                new Product{ ProductID = 2, Name = "Product 2", Type = "Type 1", InStock = 10, Price = 20 },
                new Product{ ProductID = 3, Name = "Product 3", Type = "Type 2", InStock = 8, Price = 2000 },
                new Product{ ProductID = 4, Name = "Product 4", Type = "Type 2", InStock = 5, Price = 100 }
            };

            return products;
        }

        [TestMethod]
        public void Test_Index_Returns_Elements()
        {          
            // arrange
            Mock<IProductService> mock = new Mock<IProductService>();
            mock.Setup(m => m.GetAllProducts()).Returns(GetProducts());

            var controller = new HomeController(mock.Object);

            // act
            var result = controller.Index() as ViewResult;

            // assert
            var model = result.ViewData.Model as List<Product>;

            Assert.AreEqual(4, model.Count);
        }

        // AddProduct returns a Json result with success or fail
        [TestMethod]
        public void Test_Add_Product_Ajax()
        {
            Mock<IProductService> mock = new Mock<IProductService>();
            // map product to productviewmodel
            Mapper.CreateMap<Product, ProductViewModel>();
            var productMayorA100 = Mapper.Map<Product, ProductViewModel>(GetProducts().First());
            var productMenorA100 = Mapper.Map<Product, ProductViewModel>(GetProducts().Where(p => p.ProductID == 2).Single());

            mock.Setup(m => m.AddProduct(It.Is<ProductViewModel>(p => p.Price > 100))).Returns(false);
            mock.Setup(m => m.AddProduct(It.Is<ProductViewModel>(p => p.Price <= 100))).Returns(true);


            HomeController controller = new HomeController(mock.Object);     
    
            //act     
            var result1 = controller.AddProduct(productMayorA100) as JsonResult;
            var result2 = controller.AddProduct(productMenorA100) as JsonResult;


            //assert     
            var serializer = new JavaScriptSerializer();
            var output1 = serializer.Serialize(result1.Data);
            var output2 = serializer.Serialize(result2.Data);

            Assert.AreEqual(@"{""Success"":false}", output1);
            Assert.AreEqual(@"{""Success"":true}", output2);
        }

    }
}
