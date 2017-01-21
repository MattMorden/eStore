using eStoreWebsite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace eStoreWebsite.Controllers
{
    public class LoginController : Controller
    {
        private IAuthenticationManager AuthenticationManager;

        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        //
        // POST:/Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync(UserModel model)
        {
            ActionResult response = null; //pass success msg or error string back to the ajax form
            try
            {
                AuthenticationManager = Request.GetOwinContext().Authentication; //authentication manager via Request Class
                bool loginOK = await model.LoginAsync(AuthenticationManager);

                if (loginOK)
                {
                    Session["userid"] = model.UserID;
                    Session["message"] = "Welcome " + model.UserName;
                    Session["loginstatus"] = "logged in as " + model.UserName;
                    response = Content("Success");
                }
                else
                {
                    response = Content("login failed - try again");
                }
            }//end try
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null) { Debug.WriteLine("Error - " + ex.InnerException.Message); }
                else { Debug.WriteLine("Error - " + ex.Message); }
                response = Content("A major error has occurred - contact tech support - tech@mystore.com");
            }//end catch
            return response;
        }//end LoginAsync


        //
        // GET: /Login/
        public ActionResult Login()
        {
            ViewBag.Message = "Login";
            return View();
        }
        //
        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            ActionResult response = null;
            response = Content("Logging in " + model.UserName);
            return response;
        }


        //
        // POST: /Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session.Abandon();
            IAuthenticationManager mgr = Request.GetOwinContext().Authentication;
            mgr.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }//end Logout





	}//end class
}//end namespace