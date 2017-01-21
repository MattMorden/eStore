using eStoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eStoreWebsite.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Register/
        public ActionResult Register()
        {
            ViewBag.Message = "Register";
            return View();
        }

        ////
        //// POST: /Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(UserModel model)
        {
            ActionResult response = null;
            try
            {
                string newid = await model.Register();
                Guid guidOut;

                if (Guid.TryParse(newid, out guidOut)) //is string a valid GUID?
                {
                    response = Content("Customer " + model.UserName + " Registered! Proceed to Login.");
                }
                else
                {
                    response = Content("Customer " + model.UserName + " NOT Registered! Username already taken.");
                }
            }//end try
            catch (Exception ex)
            {
                response = Content("Customer not registered! - error " + ex.Message);
            }//end catch

            return response;
        }//end Register


	}//end class
}//end namespace