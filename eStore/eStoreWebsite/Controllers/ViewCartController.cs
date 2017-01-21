using EStoreDAL;
using eStoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eStoreWebsite.Controllers
{
    public class ViewCartController : Controller
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Index/
        public ActionResult ViewCart()
        {
            return View();
        }


        public async Task<ActionResult> genOrder()
        {
            OrderModel model = new OrderModel();
            model.UserID = Convert.ToString(Session["userid"]);
            string retVal = "";

            try
            {
                bool addOK = await model.AddOrderAsync((CartItemDTO[])Session["cart"],
                                                        (double)Session["orderamt"]);
                        if(model.OrderID > 0) //Order added
                        {
                            retVal = "Order " + model.OrderID + " Created!";
                            if(model.BackOrderFlag > 0)
                            {
                                retVal += "Some goods were backordered!";
                            }
                        }
                        else //problem
                        { 
                            retVal = model.Message + ", try again later!";
                        }
            }
            catch(Exception ex)//BIG PROBLEM!
            {
                retVal = "Order was not created, try again later! - " + ex.Message;
            }//end catch
            Session.Remove("cart"); // clear out current cart once order has been placed
            return Content(retVal);

        }//end genOrder



    }// end class
}//end namespace