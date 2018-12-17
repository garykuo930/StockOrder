using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockOrderMVC.Models;

namespace StockOrderMVC.Controllers
{
    public class StockOrderController : Controller
    {
        // GET: StockOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrders()
        {
            List<StockOrder> orderlist = new List<StockOrder>();
            Random rn = new Random();

            for (int i = 0; i < 20; i++)
            {
                StockOrder order = new StockOrder();
                order.UserID = "buyer" + i;
                order.OrderType = "buying";
                order.TargetPrice = rn.Next(95, 105);
                order.Amount = rn.Next(10, 20);
                order.OrderTime = DateTime.Now;

                orderlist.Add(order);
            }

            for (int i = 0; i < 20; i++)
            {
                StockOrder order = new StockOrder();
                order.UserID = "seller" + i;
                order.OrderType = "selling";
                order.TargetPrice = rn.Next(95, 105);
                order.Amount = rn.Next(10, 20);
                order.OrderTime = DateTime.Now;

                orderlist.Add(order);
            }

            orderlist.Sort();

            var q = orderlist.GroupBy(o => o.TargetPrice).Select(o => new StockOrderVM
            {
                TargetPrice = o.Key,
                BuyAmount = o.Where(od => od.OrderType == "buying").Sum(od => od.Amount),
                SellAmount = o.Where(od => od.OrderType == "selling").Sum(od => od.Amount)
            });

            Session["OrderList"] = q.ToList();
            return PartialView(q);
        }

        public ActionResult CallAuction()
        {
            List<StockOrderVM> orderlist = Session["OrderList"] as List<StockOrderVM>;

            int AccBuying = 0;
            int AccSelling = orderlist.Sum(o => o.SellAmount);
            int index = 0;
            decimal CheckPoint = 1;
            decimal tempCheckPoint = 0;

            while (tempCheckPoint - CheckPoint < 0 )
            {
                AccBuying += orderlist[index].BuyAmount;

                CheckPoint = tempCheckPoint;

                tempCheckPoint = Math.Abs(AccBuying - AccSelling) * orderlist[index].TargetPrice;

                if(CheckPoint == 0)
                {
                    CheckPoint = tempCheckPoint + 1;
                }

                AccSelling -= orderlist[index].SellAmount;
                index++;
            }

            ViewBag.FinalPrice = orderlist[index-1].TargetPrice;

            return PartialView();
        }

        public ActionResult CallByTrade()
        {
            List<StockOrderVM> orderlist = TempData["OrderList"] as List<StockOrderVM>;


            return PartialView();
        }
    }
}