using prjMvcDemo.Models;
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
        public ActionResult Edit(int id)
        {

            List<CShoppingCartItem> list = Session[CDicition.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
            list.RemoveAt(id);
            return RedirectToAction("CartView");
        }
        public ActionResult CartView()
        {
            List<CShoppingCartItem> list = Session[CDicition.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
            if (list == null)
            {
                return RedirectToAction("List");
            }
            return View(list);
        }
        public ActionResult AddToCart(int? id)
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
        public ActionResult AddToSession(int? id)
        {
            var q = da.tProduct.FirstOrDefault(x => x.fId == id);
            if (q == null)
                return RedirectToAction("List");
            return View(q);
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartVIewModel vModel)
        {

            var q = da.tProduct.FirstOrDefault(x => x.fId == vModel.txtFid);
            if (q == null)
                return RedirectToAction("List");
            else
            {

                List<CShoppingCartItem> list = Session[CDicition.SK_已加入購物車的_商品們_列表] as List<CShoppingCartItem>;
                //沒辦法確認上述的list是否有東西;所以還要再做一次判斷式如下
                if (list == null)
                {
                    list = new List<CShoppingCartItem>();
                    Session[CDicition.SK_已加入購物車的_商品們_列表] = list;
                }
                //如果為null就把list要=Session


                //取出list再把CShoppingCartItem物件加到裡面
                CShoppingCartItem item = new CShoppingCartItem()
                {
                    count = vModel.txtCount,
                    price = (decimal)q.fPrice,
                    productId = vModel.txtFid,
                    prouduct = q


                };
                list.Add(item);

                return RedirectToAction("List");
            }
        }
    }
}