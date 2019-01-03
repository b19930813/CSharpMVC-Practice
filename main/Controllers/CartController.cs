using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        public ActionResult AddToCart(int id)
        {
            var currentCart = Models.Cart.Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }
    }
}