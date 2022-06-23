using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        dbDemoEntities da = new dbDemoEntities();
        // GET: Product
        public ActionResult List()
        {
            string key = Request.Form["txtKeyword"];
            if (key != null)
            {
                var q = da.tProduct.Where(x => x.fName.Contains(key));
                return View(q);
            }
            else
            {
                var q = da.tProduct;
                return View(q);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct p)
        {
            da.tProduct.Add(p);
            da.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            var q = da.tProduct.FirstOrDefault(x => x.fId == id);
            if (q != null)
            {
                da.tProduct.Remove(q);
                da.SaveChanges();              
            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            var q = da.tProduct.FirstOrDefault(x=>x.fId==id);
                return View(q);
        }
        [HttpPost]
        public ActionResult Edit(tProduct p)
        {
            var q = da.tProduct.FirstOrDefault(x => x.fId == p.fId);
            q.fName = p.fName;
            q.fCost = p.fCost;
            q.fPrice = p.fPrice;
            q.fQty = p.fQty;
            da.SaveChanges();
            return RedirectToAction("List");
        }
    }
}