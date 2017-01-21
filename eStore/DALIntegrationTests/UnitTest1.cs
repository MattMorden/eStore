using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using EStoreDAL;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace DALIntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task ProductServiceGetAll_ShouldReturnProductCollection(){

            ProductService _svc = new ProductService(new AppDbContext());
            ICollection<Product> allProducts = await _svc.GetAllAsync();
            Assert.IsInstanceOfType(allProducts, typeof(ICollection<Product>));
            
        }
    }
}
