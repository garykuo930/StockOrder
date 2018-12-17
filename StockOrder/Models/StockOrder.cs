using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockOrderMVC.Models
{
    public class StockOrder:IComparable<StockOrder>
    {
        //使用者帳號
        public string UserID { get; set; }
        //買單or賣單
        public string OrderType { get; set; }
        //目標價格
        public decimal TargetPrice { get; set; }
        //數量
        public int Amount { get; set; }
        //下單時間
        public DateTime OrderTime { get; set; }

        public int CompareTo(StockOrder comparer)
        {
            if(comparer == null)
            {
                return 1;
            }
            else
            {
                return comparer.TargetPrice.CompareTo(this.TargetPrice);
            }
        }
    }
}