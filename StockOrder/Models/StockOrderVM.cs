using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockOrderMVC.Models
{
    public class StockOrderVM:IComparable<StockOrderVM>
    {
        public int AccBuyAmount { get; set; }
        public int AccSellAmount { get; set; }
        public decimal TargetPrice { get; set; }
        public int BuyAmount { get; set; }
        public int SellAmount { get; set; }

        public int CompareTo(StockOrderVM other)
        {
            if(other == null)
            {
                return 1;
            }
            else
            {
                return this.TargetPrice.CompareTo(other.TargetPrice);
            }
        }
    }
}