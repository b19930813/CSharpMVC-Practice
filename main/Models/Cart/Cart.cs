using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//一個Cart裡面會有多個CartItems
namespace main.Models.Cart
{
    
    //序列化
    [Serializable]
    public class Cart : IEnumerable<CartItem>
    {
        public Cart()  //建構子 ，用CartItem
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        /// <summary>
        /// Get Good Amount
        /// </summary>
        
        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }
        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }

        //新建一筆資料
        public bool AddProduct(int ProductId)
        {
            var result = this.cartItems
                .Where(s => s.Id == ProductId)
                .Select(s => s)
                .FirstOrDefault();
            //id判斷
            if (result == default(Models.Cart.CartItem))
            {
                using (Models.mainEntities db =new mainEntities())
                {
                    var product = (from s in db.Products where s.Id == ProductId select s).FirstOrDefault();
                    if(product != default(Models.Product))  //表示有新資料進入
                    {
                        this.AddProduct(product);
                    }
                }
            }
            else
            {
                //已存在
                result.Quantity += 1;
            }
            return true;
        }
        
        private bool AddProduct(Product product)
        {
            var cartItem = new Models.Cart.CartItem()
            {
                Id = product.Id,
                name = product.Name,
                Price = product.Price,
                Quantity = 1
            };
            // Add goods to cart
            this.cartItems.Add(cartItem);
            return true;
        }

        public bool RemoveProduct(int ProductId)
        {

                var result = this.cartItems.Where(s => s.Id == ProductId).Select(s => s).FirstOrDefault();
                if (result == default(Models.Cart.CartItem))
                {
                    //Cart has no goods 
                }
                else
                {
                    //Remove goods
                    this.cartItems.Remove(result);
                }
                return true;
        }
        public bool ClearCart()
        {
            try
            {
                this.cartItems.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }


        // Order Methods
        public List<Models.OrderDetail>ToOrderDetailList(int orderId)
        {
            var result = new List<Models.OrderDetail>();
            foreach(var cartItem in this.cartItems)
            {
                result.Add(new Models.OrderDetail(){
                    Name = cartItem.name,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity,
                    OrderId = orderId
                });
            }
            return result;
        }

        #region IEnumerator

        IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion
    }

}