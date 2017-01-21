using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;
using EStoreDAL;
using eStoreWebsite;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Transactions;
namespace DALIntegrationTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public async Task OrderServiceAddOrder_ShouldCreateOrderAndItems()
        {
            //use some hardcoded order data
            string[] prodcds = new string[] { "N_Spice1", "N_Spice2" };
            int[] qty = new int[] { 1, 1 };
            Decimal[] sellPrice = new Decimal[] { 123.45M, 156.45M };

            OrderService _svc = new OrderService(new AppDbContext());
            using (TransactionScope transaction =
                    new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //need a valid userid guid for test
                //MAKE SURE THIS IS A VALID GUID FROM Asp.NetUsers table
                Dictionary<string, Object> results = await _svc.AddOrderAsync(qty, prodcds, sellPrice, "b83f4391-e1a2-44f2-a2a7-110f24361770", 316.29M);
                int OrderID = Convert.ToInt32(results["orderid"]);
                Order newOrder = await _svc.GetAsync(OrderID);
                Assert.IsTrue(newOrder.OrderLineitems.Count == 2);
                transaction.Dispose(); //should rollback
            }
        }//end test

    }//end class
}//end namespace
