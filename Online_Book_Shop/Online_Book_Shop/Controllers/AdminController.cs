using Online_Book_Shop.DAL;
using Online_Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Shop.Controllers
{
    public class AdminController : Controller
    {

        private OnlineBookStoreContext db = new OnlineBookStoreContext();
        // GET: Admin
        public ActionResult Index()
        {
            string username = Session["UserName"].ToString().ToLower(); ;
            if (username != "admin")
            {
               return RedirectToAction("Index", "Home");
            }
            
            List<Book> lstbooks = new List<Book>();
            //lstbooks = db.Books
            var books = from s in db.Books
                           select s;
           
            
            return View(books);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            string username = Session["UserName"].ToString().ToLower(); ;
            if (username != "admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var book = db.Books.Find(id);

            return View(book);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            string username = Session["UserName"].ToString().ToLower(); ;
            if (username != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(Book bookObj)
        {
            try
            {
                if (bookObj != null)
                {
                    bookObj.createdOn = DateTime.UtcNow.ToString();
                    db.Books.Add(bookObj);
                    db.SaveChanges();

                    ViewBag.BookAddedMsg = "Book Added Successfully!!";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            string username = Session["UserName"].ToString().ToLower(); ;
            if (username != "admin")
            {
                return RedirectToAction("Index", "Home");
            }


            var book = db.Books.Find(id);

            return View(book);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book bookObj)
        {
            try
            {
                if (bookObj != null)
                {
                    bookObj.createdOn = DateTime.UtcNow.ToString();
                    db.Books.Add(bookObj);
                    db.SaveChanges();

                    ViewBag.BookAddedMsg = "Book Updated Successfully!!";
                }

                return RedirectToAction("Index");

                
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            string username = Session["UserName"].ToString().ToLower(); ;
            if (username != "admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
