using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EStoreDAL;
using eStoreWebsite.Models;
using System.Threading.Tasks;

namespace eStoreWebsite.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public async Task<ActionResult> Index()
        {

            if (Session["cart"] == null)
            {
                try
                {
                    ProductModel model = new ProductModel();
                    List<ProductModel> models = await model.GetAllAsync();
                    if (models.Count() > 0)
                    {
                        CartItemDTO[] myCart = new CartItemDTO[models.Count];
                        var ctr = 0;

                        foreach (ProductModel m in models)
                        {
                            CartItemDTO item = new CartItemDTO();
                            item.ProductCode = m.ProdCode;
                            item.ProductName = m.ProdName;
                            item.Graphic = m.Graphic;
                            item.Msrp = m.Msrp;
                            item.Description = m.Description;
                            item.Qty = 0;
                            myCart[ctr++] = item;

                        }
                        Session["cart"] = myCart;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalog Problem - " + ex.Message;
                }
            }
            return View();
        }

        public ActionResult AddToCart(int qty, String detailsProductcode)
        {
            CartItemDTO[] cart = (CartItemDTO[])Session["cart"];
            String retMsg = "";

            foreach (CartItemDTO item in cart)
            {
                if (item.ProductCode == detailsProductcode)
                {
                    if (qty > 0)
                    {
                        item.Qty = qty;
                        retMsg = qty + " - item(s) Added!";
                    }
                    else
                    {
                        item.Qty = 0;
                        retMsg = "item(s) Removed!";
                    }
                    break;
                }
            }
            Session["cart"] = cart;
            return Content(retMsg);
        }


    }
}