/*
 實作Cart的操作程式碼，採用Session去做設計
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;


namespace main.Models.Cart
{
    public static class Operation 
    {
        //使用Session前須加入WebMethod
        [WebMethod(EnableSession = true)]
        
        public static Models.Cart.Cart GetCurrentCart()
        {
            //先判斷HttpContext當前是否為空
            if (System.Web.HttpContext.Current != null)
            {
                //判斷Session
                if(System.Web.HttpContext.Current.Session["Cart"]==null)
                {
                    var order = new Cart();
                    System.Web.HttpContext.Current.Session["Cart"] = order;   //指定Session為order(Cart)
                }
                //return Sesstion
                return (Cart)System.Web.HttpContext.Current.Session["Cart"];
            }
            else
            {
                throw new InvalidOperationException("Session為空!");
            }
            //return Cart
        }
            
    }

}