using BusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    public class ScheduleController : Controller
    {
        private BbsDbContext db = new BbsDbContext();

        // GET: Schedule
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var Schedules = db.Schedules.ToList();
            return View(Schedules);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            Schedule schedules = new Schedule
            {
                SourceBusStop = "",
                DestinationBusStop = "",
                SourceBusStopId = "",
                DestinationBusStopId = "",
                BusId = 0
            };
            return View(schedules);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule)
        {
            if(ModelState.IsValid)
            {
                Route r = db.Routes.Find(schedule.RouteId);
                if(r == null)
                {
                    ViewBag.Message = "RouteId is Invalid";
                    return View("Error");
                }
                schedule.SourceBusStop = r.SourceBusStop;
                schedule.DestinationBusStop = r.DestinationBusStop;
                schedule.SourceBusStopId = r.SourceBusStopId;
                schedule.DestinationBusStopId = r.DestinationBusStopId;
                schedule.BusId = r.BusId;
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index","Schedule");
            }
            return View(schedule);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int id) 
        {
            var schedules = db.Schedules.Find(id);

            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View(schedules);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Schedule schedules) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedules).State= EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(schedules);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var schedules = db.Schedules.Include(d => d.BusInfo).First(s => s.ScheduleId == id);

            if (schedules == null)
            {
                return HttpNotFound();
            }
            return View(schedules);
        }

        [HttpPost , ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var schedules = db.Schedules.Find(id);

            if (schedules == null)
            {
                return HttpNotFound();
            }

                db.Schedules.Remove(schedules);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        [Authorize(Roles ="Passenger")]
        [HttpPost]
        public ActionResult SearchSchedule(FormCollection f)
        { 
            int SourceBusStopId = int.Parse(f["source"]);
            string SourceBusStop = db.BusStops.Find(SourceBusStopId).BusStopName;

            int DestinationBustopId = int.Parse(f["destination"]);
            string DestinationBusStop= db.BusStops.Find(DestinationBustopId).BusStopName;

            DateTime BusDate = DateTime.ParseExact(f["date"], "yyyy-MM-dd", null);

            List<Schedule> schedules = db.Schedules.Where(r => r.SourceBusStop == SourceBusStop && r.DestinationBusStop == DestinationBusStop && r.BusDate == BusDate).ToList();

            return View(schedules);
        }


    }
}