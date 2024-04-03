using BusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BusBookingSystem.Controllers
{
    public class LoginController : Controller
    {
        BbsDbContext db = new BbsDbContext();
        // GET: Login
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(User user)
        {
            //user.Role = "Passenger";
            if (ModelState.IsValid)
            {
                var check = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
                if (check != null)
                {
                    ModelState.AddModelError("", "User Already Exists");
                    return View(user);
                }
                
                db.Users.Add(user);
                var a = db.SaveChanges();

                if (a > 0)
                {

                    return RedirectToAction("SignIn", "Login");
                }

                else
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User user)
        {

            var passenger = db.Users.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();

            if (passenger != null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                Session["uname"] = passenger.FirstName;
                Session["userid"] = passenger.UserId;
                Session.Add("Role", passenger.Role);

                if (passenger.Role == "Admin")
                {
                    return RedirectToAction("AdminHome", "Home");
                }

                if (passenger.Role == "Passenger")
                {
                    return RedirectToAction("UserHome", "Home");
                }

                return RedirectToAction("Index", "Route");
            }
            else
            {
                return View();
            }


        }
        public ActionResult SignOut(User user)
        {
            FormsAuthentication.SignOut();
            Session["uname"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}