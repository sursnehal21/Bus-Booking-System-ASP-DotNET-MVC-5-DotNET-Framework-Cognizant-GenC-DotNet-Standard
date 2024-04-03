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
    public class BusInfoController : Controller
    {
        private BbsDbContext db = new BbsDbContext();

        // GET: BusInfo
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var busInfos = db.BusInfos.ToList();
            return View(busInfos);
        }



        //Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusInfo busInfos)
        {
            if (ModelState.IsValid)
            {
                db.BusInfos.Add(busInfos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(busInfos);
        }

        //Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var busInfos = db.BusInfos.Find(id);

            if (busInfos == null)
            {
                return HttpNotFound();
            }
            return View(busInfos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BusInfo busInfos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busInfos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(busInfos);
        }

        //Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var busInfos = db.BusInfos.Find(id);
            if (busInfos == null)
            {
                return HttpNotFound();
            }
            return View(busInfos);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var busInfos = db.BusInfos.Find(id);
            if (busInfos == null)
            {
                return HttpNotFound();
            }

            db.BusInfos.Remove(busInfos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
