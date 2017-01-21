using eStoreWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eStoreWebsite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //just put out a couple of cosmetics on the home page if they're not logged in
            if(Session["loginstatus"] == null)
            {
                Session["loginstatus"] = "not logged in";
            }
            if((string)Session["loginstatus"] == "not logged in")
            {
                Session["message"] = "most functionality requires you to login!";
            }
            return View();
        }


        public ActionResult Home()
        {
            return View();
        }





        }//end class              
	}//end namespace
