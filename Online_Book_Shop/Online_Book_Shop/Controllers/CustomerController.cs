using Online_Book_Shop.DAL;
using Online_Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Shop.Controllers
{
    public class CustomerController : Controller
    {

        private OnlineBookStoreContext db = new OnlineBookStoreContext();
        List<Book> cartlst = new List<Book>();
        List<Book> lstbooks = new List<Book>();
        // GET: Customer
        public ActionResult Index()
        {
            //lstbooks = db.Books
            var books = from s in db.Books
                        select s;

            return View(books);

        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            var book = db.Books.Find(id);

            return View(book);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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

        public ActionResult AddToCart(int id)
        {

            var book = db.Books.Find(id);

            if (Session["cart"] != null)
            {
                cartlst = Session["cart"] as List<Book>;
            }

            if (cartlst.Count > 0)
            {

                var temCartlst = cartlst;

                bool alreadyExists = cartlst.Any(x => x.Id == id);

                if (alreadyExists)
                {
                    ViewBag.bookExist = "Book already added to the cart";
                }
                else {
                    cartlst.Add(book);
                }

               /* foreach (var bookobj in temCartlst)
                {
                    if (bookobj.Id != id)
                    {
                        
                    }
                    else {
                        
                    }
                }*/
            }
            else {
                cartlst.Add(book);
            }
            
            ViewBag.cartlst = cartlst;

            Session["cart"] = cartlst;

            ViewBag.cartcount = cartlst.Count;

            var books = from s in db.Books
                        select s;

            return View("Index", books);
        }

        public ActionResult ClearCart()
        {
            cartlst.Clear();
            Session["cart"] = null;
            var books = from s in db.Books
                        select s;

            return View("Index", books);

        }
    }
}
