using BusBookingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    [Authorize(Roles ="Passenger")]
    public class BookingController : Controller
    {
        private BbsDbContext db=new BbsDbContext();
        // GET: Booking
        public ActionResult Index()
        {
            int userID = (int)Session["userid"];
            var bookings = db.Bookings.Where(b=>b.UserId == userID).ToList();
            return View(bookings);
        }
        

        public ActionResult PayNow(Booking booking)
        {
            Payment pay = new Payment();
            pay.PaymentDate = DateTime.Now;
            pay.UserId = booking.UserId;
            User u = db.Users.Find(booking.UserId);
            pay.Username = u.FirstName + " " + u.LastName;
            pay.BookingId = booking.BookingId;
            pay.ScheduleId = booking.ScheduleId;
            //pay.PaymentMode = "Online";
            var s = db.Schedules.Find(booking.ScheduleId);
            pay.Amount = (decimal)(booking.NumberOfPassengers * s.Price);
            Session.Add("Booking", booking);
            return View(pay);

        }

       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult PayNow(Payment pay)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Payment Process Completed";
                Booking booking = (Booking)Session["Booking"];
                return RedirectToAction("Confirmation", new { id = booking.BookingId });
            }
            ViewBag.Message = "Payment Failed";
            return View("Error");

        }

        //GET:Booking/Create
        public ActionResult Create(int Id)
        {
            int UserID = (int)Session["userid"];
            ViewBag.UserID = UserID;
            Schedule schedule = db.Schedules.Find(Id);

            if (schedule == null)
            {
                return HttpNotFound();
            }

            Booking booking = new Booking()
            {
                Source = schedule.SourceBusStop,
                Destination = schedule.DestinationBusStop,
                BookingDate = DateTime.Now,
                RouteId = schedule.RouteId,
                UserId = UserID,
                ScheduleId = schedule.ScheduleId
            };
            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Booking booking)
        {
            if (ModelState.IsValid)
            {

                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("PayNow", booking);

            }

            // If there are validation errors, return to the create view with the model
            return View(booking);
        }

        // GET: Booking/Confirmation
        public ActionResult Confirmation(string id)
        {
            int userId = (int)Session["userid"];
            int maxBookId = db.Bookings.Where(b => b.UserId == userId).Select(b => b.BookingId).Max();
            Booking booking = null;
            if (id != null)
            {
                int Bid = Int32.Parse(id);
                booking = db.Bookings.Find(Bid);

            }
            else
            {
                booking = db.Bookings.Find(maxBookId);
            }
            if (booking == null)
            {
                return HttpNotFound();
            }

            var s = db.Schedules.Find(booking.ScheduleId);

            Route f = db.Routes.Find(booking.RouteId);
            User u = db.Users.Find(booking.UserId);

            float totalPrice = booking.NumberOfPassengers * s.Price;
            BusInfo al = db.BusInfos.Find(s.BusId);


            ViewBag.FirstName = u.FirstName;
            ViewBag.LastName = u.LastName;
            ViewBag.Source = booking.Source;
            ViewBag.Destination = booking.Destination;
            ViewBag.BusName = al.BusName;
            ViewBag.ArrivalTime = s.ArrivalTime;
            ViewBag.DepartureTime = s.DepartureTime;
            ViewBag.BookingDate = booking.BookingDate.ToShortDateString();
            ViewBag.TotalPrice = totalPrice;
            ViewBag.BookingId = booking.BookingId;
            ViewBag.NumberOfPassengers=booking.NumberOfPassengers;
            ViewBag.RouteId= booking.RouteId;
            ViewBag.BusDate = booking.Schedule.BusDate.ToShortDateString();

            return View();
        }

        public ActionResult Cancel(int id)
        {
            Booking booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            // Delete the booking record from the database
            db.Bookings.Remove(booking);
            db.SaveChanges();

            return RedirectToAction("CancellationConfirmation");
        }

        // GET: Booking/CancellationConfirmation
        public ActionResult CancellationConfirmation()
        {
            return View();
        }

    }
}