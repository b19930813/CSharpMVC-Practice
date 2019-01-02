using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //修正為抓取商品
            //return View();
            using (Models.mainEntities db =new Models.mainEntities())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result); 
            }
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Index2()
        {
            return Content(
                "<html><body><h1>Test Message</h1></body></html>"
            );
        }
    }
}