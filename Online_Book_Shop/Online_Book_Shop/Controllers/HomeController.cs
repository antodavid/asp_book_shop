using Online_Book_Shop.DAL;
using Online_Book_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Book_Shop.Controllers
{
    public class HomeController : Controller
    {
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application
        private OnlineBookStoreContext db = new OnlineBookStoreContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(User userObj)
        {
            if (userObj != null)
            {

                User usr = db.Users.SingleOrDefault(x => x.UserName == userObj.UserName && x.Password == userObj.Password);

                if (usr != null)
                {
                    Session["UserName"] = usr.UserName.ToString();

                    if (usr.UserName.ToLower() == "admin")
                    {
                       return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Customer");
                    }
                }
                else
                {
                    ViewBag.InvalidLogin = "Invalid Login";
                    return View("Login");
                }

            }
            {

            }
            return View("Login");
        }

        public ActionResult SignUp()
        {

            return View();
        }

        public ActionResult ValidateSignup(User userObj)
        {
            try
            {

                if (userObj != null)
                {

                    User user = db.Users.SingleOrDefault(x => x.UserName == userObj.UserName);

                    if (user == null)
                    {
                        db.Users.Add(userObj);
                        db.SaveChanges();
                        Session["UserName"] = userObj.UserName.ToString();
                        ViewBag.SignupSuccess = "User SignedUp Successfully!!";
                        return RedirectToAction("Index","Customer");
                    }
                    else
                    {
                        ViewBag.InvalidUser = "User already exists!!";
                        return View("SignUp");
                    }
                }
                else
                {
                    return View("SignUp");
                }
            }
            catch (Exception ex)
            {
                return View("SignUp");
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["cart"] = null;
            Session["amount"] = null;
            ViewBag.SignupSuccess = "You are successfuly Logged Out!!";
            return View("Login");
        }

        public ActionResult ResetPassword()
        {
            UpdateUser usr= new UpdateUser();
            return View(usr);
        }

        [HttpPost]
        public ActionResult ResetPassword(UpdateUser updateusr)
        {

            var UpdateUsr=db.Users.SingleOrDefault(x => x.UserName == updateusr.Username);

            if (UpdateUsr != null)
            {
                if (updateusr.Code == "321")
                {
                    UpdateUsr.Password = updateusr.NewPassword;
                    db.Users.Add(UpdateUsr);
                    db.Entry(UpdateUsr).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.invalidUsr = "Password updated successfully go and login!!";
                }
                else {
                    ViewBag.invalidUsr = "Invalid Code";
                }

            }
            else {

                ViewBag.invalidUsr = "Invalid User";
            
            }

            return View();
        }



    }
}