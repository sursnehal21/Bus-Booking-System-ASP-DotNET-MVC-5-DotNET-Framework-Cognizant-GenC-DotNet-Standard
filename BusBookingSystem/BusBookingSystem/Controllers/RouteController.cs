using BusBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingSystem.Controllers
{
    public class RouteController : Controller
    {
        private BbsDbContext db = new BbsDbContext();


        // GET: Route
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var routes=db.Routes.ToList();
            return View(routes);
        }

        //Get:Route/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.SourceBusStop= new SelectList(db.BusStops, "BusStopId", "BusStopName");
            ViewBag.DestinationBusStop = new SelectList(db.BusStops, "BusStopId", "BusStopName");
            ViewBag.BusInfos = new SelectList(db.BusInfos, "BusId", "BusName");
            ViewBag.BusStops = new SelectList(db.BusStops, "BusStopId", "BusStopName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection route)
        {
            try
            {
                Route r = new Route();

                int SourceBusStop = int.Parse(route["BusStopId"]);
                int DestinationBusStop = int.Parse(route["BusStopId1"]);

                BusStop arp = db.BusStops.Where(ar => ar.BusStopId == SourceBusStop).FirstOrDefault();
                String Source = arp.City;

                BusStop arp1 = db.BusStops.Where(ar1 => ar1.BusStopId == DestinationBusStop).FirstOrDefault();
                String Destination = arp1.City;

                r.BusId = int.Parse(route["BusId"]);
                BusInfo a = db.BusInfos.Find(r.BusId);

                r.TotalSeatsCapacity = 0;
                r.SourceBusStopId = Source;
                r.DestinationBusStopId = Destination;

                r.BusStopId = int.Parse(route["BusStopId"]);

                r.SourceBusStop = arp.BusStopName;
                r.DestinationBusStop = arp1.BusStopName;
                //string Atime = null;

                r.ArrivalTime = "0";
                r.DepartureTime = "0";

                db.Routes.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //GET:Route/Edit
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int id)
        {
            var route = db.Routes.Find(id);

            if(route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Route route) 
        {

            if (ModelState.IsValid)
            {
                db.Entry(route).State=EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(route);
        }

        //Get:Route/Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var route = db.Routes.Include(d => d.BusInfo).First(r => r.RouteId == id);

            if (route == null)
            {
                return HttpNotFound();
            }
            return View(route);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var route = db.Routes.Find(id);

            if (route == null)
            {
                return HttpNotFound();
            }

            db.Routes.Remove(route);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}