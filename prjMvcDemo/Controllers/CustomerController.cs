using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> list = null;
            if (string.IsNullOrEmpty(keyword))
                list = (new CCustomerFactory()).queryAll();
            else
                list = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(list);
        }
        public ActionResult Delete(int? id)
        {
             (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }
        public ActionResult New()
        {           
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).insert(x);
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            CCustomer x=(new CCustomerFactory()).queryById((int)id);
            return View(x);
        }
        [HttpPost]
        public ActionResult Edit(CCustomer x)
        {
            //CCustomer x = new CCustomer();
            //x.fName = Request.Form["txtName"];
            //x.fPhone = Request.Form["txtPhone"];
            //x.fEmail = Request.Form["txtEmail"];
            //x.fAddress = Request.Form["txtAddress"];
            //x.fPassword = Request.Form["txtPassword"];
            //(new CCustomerFactory()).insert(x);
            //return RedirectToAction("List");
            //CCustomer x 當參數傳遞
            //x.fName = Request.Form["txtName"];把物件資料當作name ! class裡面設定什麼名稱name就用什麼名稱 ex: name="fName"
            //===========================以上偷雞摸狗法可以不用給值
            (new CCustomerFactory()).Update(x);
            return RedirectToAction("List");
        }
    }
}