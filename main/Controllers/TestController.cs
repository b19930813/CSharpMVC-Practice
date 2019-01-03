using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        
        public ActionResult GetCart()
        {
            //Get Current Cart
            var cart = Models.Cart.Operation.GetCurrentCart();
            cart.AddProduct(1);
            return Content(String.Format("目前購物車總價為:{0}元",cart.TotalAmount));
        }
        
    }
}