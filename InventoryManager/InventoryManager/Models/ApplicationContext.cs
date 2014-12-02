using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using InventoryManager.Models.Entities;

namespace InventoryManager.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("InventoryDB") {
            System.Diagnostics.Debug.WriteLine("{0} instances of ApplicationContext", ++count);

            Database.SetInitializer<ApplicationContext>(new InventoryInitializer());
        }

        public static int count = 0;

        public DbSet<Product> Products { get; set; }
    }

    public class InventoryInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            List<Product> products = new List<Product>
            {
                new Product(){ Name = "Huawei G7300", Type = "Telefonos", Price = 3400m, InStock = 50 },
                new Product(){ Name = "Samsung Galaxy S5", Type = "Telefonos", Price = 10500m, InStock = 20 },
                new Product(){ Name = "Playstation 4", Type = "Consolas", Price = 20800m, InStock = 20 },
                new Product(){ Name = "XBox One", Type = "Consolas", Price = 24300m, InStock = 10 }
            };

            foreach (Product p in products)
                db.Products.Add(p);

            base.Seed(db);
        }
    }
}