using BusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private BbsDbContext db = new BbsDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult AdminHome()
        {
            return View();
        }

        [Authorize(Roles = "Passenger")]
        public ActionResult UserHome()
        {
            ViewBag.SourceBusStop = db.BusStops.ToList();
            ViewBag.DestinationBusStop = db.BusStops.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UserHome(FormCollection f)
        {
            string Source = f["BusStopId"];
            string Destination = f["BusStopId1"];
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Welcome to BusBlitz, your reliable partner for seamless and efficient bus bookings. Establised in 2024, our platform is dedicated to simplifying the process of bus bookings for travelers in India.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "BusBlitz";

            return View();
        }

        public ActionResult Details()
        {
            int userId = (int)Session["userid"];
            User user = db.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


    }
}