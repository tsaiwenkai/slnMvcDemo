using prjMvcDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShippingCartController : Controller
    {
        // GET: ShippingCart
        dbDemoEntities da = new dbDemoEntities();
        public ActionResult List()
        {
            var q = da.tProduct;
            return View(q);
        }
        public ActionResult AddToCart( int? id)
        {
            var q = da.tProduct.FirstOrDefault(x => x.fId == id);
            if (q == null)
                return RedirectToAction("List");
            return View(q);
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartVIewModel vModel)
        {
            tShippingCart cart = new tShippingCart();
            var q = da.tProduct.FirstOrDefault(x => x.fId == vModel.txtFid);
            if (q == null)
                return RedirectToAction("List");
            else
            {
                cart.fPrductId = vModel.txtFid;
                cart.fDate = DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");
                cart.fCustomerId = 1;
                cart.fCount = vModel.txtCount;
                cart.fPrice = q.fPrice;
                da.tShippingCart.Add(cart);
                da.SaveChanges();
                return RedirectToAction("List");
            }

           
        }
    }
}