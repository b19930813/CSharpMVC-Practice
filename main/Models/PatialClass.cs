using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Models
{
    public class PatialClass
    {    
    }

    //定義Models
    public partial class Order
    {
        //Get Order Username
        public string GetNeckName()
        {
            using (Models.UserEntities db =new UserEntities())
            {
                var result = (from s in db.AspNetUsers where s.Id == this.UserId select s.UserName).FirstOrDefault();
                return result.ToString();
            }
        }
    }
}