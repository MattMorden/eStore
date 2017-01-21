using EStoreDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


//Adding both orders and line items so we need to encompass both types of adds in a transaction. 
namespace EStoreDAL
{
    public class OrderService : BaseService<Order>
    {        
        public OrderService(AppDbContext ctx) : base(ctx) { }
        
        public async Task<Dictionary<string,Object>> AddOrderAsync(int[] qty, string[] prodcd,
                                                                    Decimal[] sellPrice, string uid,
                                                                    Decimal oTotal)
        {
            var boFlg = false;
            Dictionary<string, Object> dictionaryReturnValues = new Dictionary<string, Object>();

            //Define a transaction scope for the operations. Since context is in Base Service don't use 
            //new BeginTransaction code at this level
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Order myOrder = new Order();

                //build the order
                myOrder.OrderDate = DateTime.Now;
                myOrder.OrderAmount = oTotal;
                myOrder.UserID = uid;
                myOrder = await AddAsync(myOrder);
                for(var idx = 0; idx < qty.Length; idx++)
                {
                    if(qty[idx] > 0)
                    {
                        OrderLineitem item = new OrderLineitem();
                        var pcd = prodcd[idx];
                        ProductService prodSvc = new ProductService(_context);
                        item.Product = await prodSvc.FindAsync(p => p.ProductID == pcd);

                        if(item.Product.QtyOnHand > qty[idx]) //enough stock
                        {
                            item.Product.QtyOnHand = item.Product.QtyOnHand - qty[idx];
                            item.QtySold = qty[idx];
                            item.QtyOrdered = qty[idx];
                            item.QtyBackOrdered = 0;
                            item.SellingPrice = sellPrice[idx];
                        }//end if
                        else //not enough stock
                        {
                            item.QtyBackOrdered = qty[idx] - item.Product.QtyOnHand;
                            item.Product.QtyOnBackOrder = item.Product.QtyOnBackOrder + (qty[idx] - item.Product.QtyOnHand);
                            item.Product.QtyOnHand = 0;
                            item.QtyOrdered = qty[idx];
                            item.QtySold = item.QtyOrdered - item.QtyBackOrdered;
                            item.SellingPrice = sellPrice[idx];
                            boFlg = true; // something backordered
                        }//end else
                        myOrder.OrderLineitems.Add(item);
                    }//end if
                }//end for

                myOrder = await UpdateAsync(myOrder, myOrder.OrderID);
                //test trans by uncommenting out these 3 lines
                //int x = 1;
                //int y = 0;
                //x = x / y;

                //The complete method commits the transaction. If an exception has been thrown,
                //Complete is not called and the transaction is rolled back.
                transaction.Complete();

                dictionaryReturnValues.Add("orderid", myOrder.OrderID);
                dictionaryReturnValues.Add("boflag", boFlg);
                return dictionaryReturnValues;
            }//end using
        }//end AddOrderAsync




    }//end class
}//end namespace
