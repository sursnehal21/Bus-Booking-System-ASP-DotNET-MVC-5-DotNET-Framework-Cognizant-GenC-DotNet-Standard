using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusBookingSystem.Models;

namespace BusBookingSystem.Controllers
{
    public class BusStopController : Controller
    {
        private BbsDbContext db = new BbsDbContext();

        // GET: BusStop
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var busStops = db.BusStops.ToList();
            return View(busStops);
        }



        // GET: BusInfoes/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusInfoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusStop busStops)
        {
            if (ModelState.IsValid)
            {
                if (db.BusStops.Any(b => b.City == busStops.City))
                {

                }
                db.BusStops.Add(busStops);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(busStops);
        }

        // GET: BusInfoes/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var busStops = db.BusStops.Find(id);

            if (busStops == null)
            {
                return HttpNotFound();
            }
            return View(busStops);
        }

        // POST: BusInfoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusStop busStops)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busStops).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(busStops);
        }

        // GET: BusInfoes/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var busStops = db.BusStops.Find(id);
            if (busStops == null)
            {
                return HttpNotFound();
            }
            return View(busStops);
        }

        // POST: BusInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var busStops = db.BusStops.Find(id);
            if (busStops == null)
            {
                return HttpNotFound();
            }

            db.BusStops.Remove(busStops);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
