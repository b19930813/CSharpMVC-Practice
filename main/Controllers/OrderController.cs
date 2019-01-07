﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace main.Controllers
{
    public class OrderController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        // GET: Order  帶入購物車的ship

        [HttpPost]
        public ActionResult Index(Models.OrderModel.Ship postback)
        {
            if (this.ModelState.IsValid)
            {
                var currentcart = Models.Cart.Operation.GetCurrentCart();

                var UserId = HttpContext.User.Identity.GetUserId();
                using (Models.mainEntities db = new Models.mainEntities())
                {
                    //Order object
                    var order = new Models.Order()
                    {
                        UserId = UserId,
                        RecieverName = postback.ReceiverName,
                        RecieverPhone = postback.ReceiverPhone,
                        RecieverAddress = postback.ReceiverAddress
                    };
                    //Add data table
                    db.OrderSet.Add(order);
                    db.SaveChanges();

                    //Get OrderDetail Object
                    var orderDetails = currentcart.ToOrderDetailList(order.Id);
                    // insert to order datatable
                    db.OrderDetailSet.AddRange(orderDetails);
                    db.SaveChanges();
                }
                return Content("訂購成功!");

            }
            return View();
        }
    }
}