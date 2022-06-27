using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMvcDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        [DisplayName("數量")]
        public int count { get; set; }
        [DisplayName("商品價格")]
        public decimal price { get; set; }
        public decimal 小計{ get { return this.count * price; } }
        public tProduct prouduct{ get; set; }

    }
}