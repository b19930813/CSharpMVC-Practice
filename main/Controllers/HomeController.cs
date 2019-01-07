using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
        //細項
        public ActionResult Details(int Id)
        {
            using (Models.mainEntities db = new Models.mainEntities())
            {
                var result = (from s in db.Products where s.Id == Id select s).FirstOrDefault();
                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
        /*
     //加入評論
     [HttpPost]  //採用post的方式送資料
     [Authorize]  //登入才可留言
     public ActionResult AddContent(int Id,string Content)
     {
         var userid = HttpContext.User.Identity.GetUserId();
         var currentDateTime = DateTime.Now;
            var comment = new Models.ProductComment()
            {
                ProductId = Id,
                Content = Content,
                UserId = userid,
                CreateDate = currentDateTime
            };
            using (Models.mainEntities db = new Models.mainEntities())
            {
                db.ProductCommentSet.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = Id });
     }
     */
    }
}