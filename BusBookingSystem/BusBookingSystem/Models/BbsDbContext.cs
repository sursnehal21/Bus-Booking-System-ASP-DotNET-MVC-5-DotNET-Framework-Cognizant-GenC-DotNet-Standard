using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusBookingSystem.Models
{
    public class BbsDbContext : DbContext
    {
        public BbsDbContext() : base("dbcon")
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BusInfo> BusInfos { get; set; }
        public DbSet<BusStop> BusStops { get; set; }

        public DbSet<Route> Routes {  get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments {  get; set; }



        



    }
}