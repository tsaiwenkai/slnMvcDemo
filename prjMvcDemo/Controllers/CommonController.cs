using prjMvcDemo.Models;
using prjMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            CCustomer cust = Session[CDicition.SK_使用者名稱] as CCustomer;
            if (cust == null)
                return RedirectToAction("Login");
            return View(cust);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLogin vmodel)
        {
            CCustomer cust = (new CCustomerFactory()).queryByEmail(vmodel.txtAccount);
            if (cust.fPassword.Equals(vmodel.txtPassword))
            {
                Session[CDicition.SK_使用者名稱] = cust.fName;
               return RedirectToAction("Home");
            }
            return View();
        }
    }
}