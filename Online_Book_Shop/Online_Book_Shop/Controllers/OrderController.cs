using Online_Book_Shop.DAL;
using Online_Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Shop.Controllers
{
    public class OrderController : Controller
    {

        private OnlineBookStoreContext db = new OnlineBookStoreContext();
        // GET: Order
        public ActionResult Index()
        {
            var orderBookslst = Session["cart"] as List<Book>;

            if (orderBookslst.Count < 0)
            {
                return View("Index", "Customer");
            }

            double totalAmount = 0;
            if (orderBookslst.Count > 0)
            {
                foreach (var book in orderBookslst)
                {
                    totalAmount = totalAmount + book.Price;
                
                }
            }
            Session["amount"] = totalAmount;
            ViewBag.OrderAmount = totalAmount;

            return View(orderBookslst);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Remove(int id)
        {

            var orderBookslst = Session["cart"] as List<Book>;
            var book = db.Books.Find(id);

            orderBookslst.RemoveAll(x=>x.Id==id);

            Session["cart"] = orderBookslst;
            ViewBag.updateCart = "Book removed from your cart!!";

            double totalAmount = 0;
            if (orderBookslst.Count > 0)
            {
                foreach (var bookobj in orderBookslst)
                {
                    totalAmount = totalAmount + bookobj.Price;

                }
            }
            Session["amount"] = totalAmount;
            ViewBag.OrderAmount = totalAmount;

            if (orderBookslst.Count > 0)
            {
                return View("Index", orderBookslst);
            }
            else
            {

                return RedirectToAction("Index", "Customer");
            }
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult PlaceOrder(double amount)
        {
            Order obj = new Order();
            obj.amount =(double) Session["amount"];
            obj.PaymentMethod = "Card";
            
            return View(obj);
        }

        [HttpPost]
        public ActionResult PlaceOrder(Order orderObj)
        {
            string username = Session["UserName"].ToString();
            User user = db.Users.SingleOrDefault(x => x.UserName == username);
            //orderObj.Books = Session["cart"] as List<Book>;
            orderObj.UserId = user.Id;            
            orderObj.OrderDate = DateTime.UtcNow.ToString();
            orderObj.Status = "Processing";
            db.Orders.Add(orderObj);
            db.SaveChanges();
            Session["cart"] = null;
            Session["amount"] = null;

            return RedirectToAction("Index", "Customer");            
        }

        public ActionResult Orderlist()
        {
            var orders = from s in db.Orders
                        select s;

            string username = Session["UserName"].ToString();
            User user = db.Users.SingleOrDefault(x => x.UserName == username);
         
            if (username != "admin")
            {
                orders = orders.Where(x => x.UserId == user.Id);
            }

            return View(orders);
        
        }


        public ActionResult UpdateOrderStatus(int id)
        {
            var order = db.Orders.Find(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrderStatus(Order orderObj)
        {

            var order = db.Orders.SingleOrDefault(x => x.Id == orderObj.Id);
            order.Status = orderObj.Status;
            db.Orders.Add(order);
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Orderlist");
        }
    }
}
