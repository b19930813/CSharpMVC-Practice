using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace main.Models.OrderModel
{
    public class Ship 
    {
        /// <summary>
        /// 收件人姓名
        /// </summary>
      [Required]
      [Display(Name = "收貨人姓名")]
      [StringLength(30,ErrorMessage = "{0}的長度至少必須為{2}字元。",MinimumLength = 2)]
      public string ReceiverName { get; set; }

        /// <summary>
        /// 電話
        /// </summary>
       [Required]
       [Display(Name = "收貨人電話")]
       [StringLength(15, ErrorMessage = "{0}的長度至少必須為{2}字元。", MinimumLength = 8)]
       public string ReceiverPhone { get; set; }

       [Required]
       [Display(Name ="收貨人地址")]
       [StringLength(256,ErrorMessage = "{0}的長度至少必須為{2}字元。",MinimumLength = 8)]
       public string ReceiverAddress { get; set; }
    }
}