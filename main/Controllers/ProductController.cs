using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Models.Product> result = new List<Models.Product>();

            // Get Success Message
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.mainEntities db =new Models.mainEntities())
            {
                //LinQ
                result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }
        // Create Product, in order to implement page
        public ActionResult Create()
        {
            return View();
        }

        //using HttpPost to Create Product
        [HttpPost]
        public ActionResult Create(Models.Product postback)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.mainEntities db = new Models.mainEntities())
                {
                    db.Products.Add(postback); 
                    db.SaveChanges();
                    //Success Message
                    TempData["ResultMessage"] = String.Format("商品[{0}]建立成功", postback.Name);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.ResultMessage = "資料有誤";
                return View();
            }

        }
        public ActionResult Edit(int id)
        {
            using(Models.mainEntities db =new Models.mainEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();   
                if(result != default(Models.Product))
                {
                    return View(result);
                }
                else
                {
                    TempData["resultMessage"] = "資料有誤，請重新操作";
                    return RedirectToAction("Index");
                }
            }
        }

        //Http
        [HttpPost]
        public ActionResult Edit(Models.Product postback)
        {
            if (this.ModelState.IsValid)
            {
                using(main.Models.mainEntities db = new Models.mainEntities())
                {
                    var result = (from s in db.Products where s.Id == postback.Id select s).FirstOrDefault();
                    //Revise Data
                 
                    result.Name = postback.Name;
                    result.Price = postback.Price;
                    result.PublishDate = postback.PublishDate;
                    result.Quantity = postback.Quantity;
                    result.Status = postback.Status;
                    result.CategoryId = postback.CategoryId;
                    result.DefaultImageId = postback.DefaultImageId;
                    result.Description = postback.Description;
                    result.DefaultImageURL = postback.DefaultImageURL;

                    //save data which changed
                    db.SaveChanges();

                    //retuen message
                    TempData["ResultMessage"] = String.Format("成功編輯");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(postback);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (Models.mainEntities db =new Models.mainEntities())
            {
                var result = (from s in db.Products where s.Id == id select s).FirstOrDefault();
                if(result != default(Models.Product))
                {
                    db.Products.Remove(result);
                    db.SaveChanges();
                    TempData["resultMessage"] = String.Format("商品[{0}]已刪除", result.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["resultMessage"] = String.Format("沒有找到指定商品，無法刪除");
                    return RedirectToAction("Index");
                }
            }
        }
    }
}