using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockOrderMVC.Models
{
    public class StockOrderVM
    {
        public decimal TargetPrice { get; set; }
        public int BuyAmount { get; set; }
        public int SellAmount { get; set; }
    }
}