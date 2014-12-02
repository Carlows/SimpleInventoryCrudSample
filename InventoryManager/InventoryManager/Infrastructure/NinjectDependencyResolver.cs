using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using InventoryManager.Models;
using InventoryManager.Infrastructure.Services;
using InventoryManager.Models.Repositories;

namespace InventoryManager.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<ApplicationContext>().ToSelf().InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IProductRepository>().To<ProductRepository>().InRequestScope();
        }
    }
}