using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            //取得Models
            using (Models.mainEntities db =new Models.mainEntities())
            {
                var result = (from s in db.OrderSet select s).ToList();
                return View(result);
            }
        }
        public ActionResult Details(int Id)
        {
            using (Models.mainEntities db =new Models.mainEntities())
            {
                var result = (from s in db.OrderDetailSet where s.Id == Id select s).ToList();
                if(result.Count == 0)
                {
                    //數量為0，異常，導回index
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }
        public ActionResult SearchByUserName(string UserName)
        {
            string searchId = null;
            using (Models.UserEntities db = new Models.UserEntities())
            {
                 searchId = (from s in db.AspNetUsers where s.UserName == UserName select s.Id).FirstOrDefault();
            }
            if (!String.IsNullOrEmpty(searchId))
            {
                using (Models.mainEntities db =new Models.mainEntities())
                {
                    var result = (from s in db.OrderSet where s.UserId == searchId select s).ToList();
                    return View("Index", result);
                }
            }
            else
            {
                return View("Index", new List<Models.Order>());
            }
        }
    }
}