namespace BusBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                        Source = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        NumberOfPassengers = c.Int(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RouteId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        BusId = c.Int(nullable: false),
                        BusStopId = c.Int(nullable: false),
                        SourceBusStop = c.String(nullable: false),
                        SourceBusStopId = c.String(),
                        DestinationBusStop = c.String(nullable: false),
                        DestinationBusStopId = c.String(),
                        DepartureTime = c.String(nullable: false),
                        ArrivalTime = c.String(nullable: false),
                        TotalSeatsCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.BusInfoes", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.BusStops", t => t.BusStopId, cascadeDelete: true)
                .Index(t => t.BusId)
                .Index(t => t.BusStopId);
            
            CreateTable(
                "dbo.BusInfoes",
                c => new
                    {
                        BusId = c.Int(nullable: false, identity: true),
                        BusName = c.String(nullable: false, maxLength: 30),
                        SeatsCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BusId);
            
            CreateTable(
                "dbo.BusStops",
                c => new
                    {
                        BusStopId = c.Int(nullable: false, identity: true),
                        BusStopName = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.BusStopId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        BusId = c.Int(nullable: false),
                        SourceBusStop = c.String(),
                        DestinationBusStop = c.String(),
                        SourceBusStopId = c.String(),
                        DestinationBusStopId = c.String(),
                        BusDate = c.DateTime(nullable: false),
                        DepartureTime = c.String(),
                        ArrivalTime = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.BusInfoes", t => t.BusId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: false)
                .Index(t => t.RouteId)
                .Index(t => t.BusId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        UserName = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 16),
                        ConfirmPassword = c.String(maxLength: 16),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PaymentDate = c.DateTime(nullable: false),
                        CardType = c.String(),
                        CardNo = c.Long(nullable: false),
                        CVV = c.Int(nullable: false),
                        PaymentMode = c.String(),
                        BookingId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.BookingId)
                .Index(t => t.UserId)
                .Index(t => t.ScheduleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Payments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Payments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Bookings", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Schedules", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Schedules", "BusId", "dbo.BusInfoes");
            DropForeignKey("dbo.Routes", "BusStopId", "dbo.BusStops");
            DropForeignKey("dbo.Routes", "BusId", "dbo.BusInfoes");
            DropIndex("dbo.Payments", new[] { "ScheduleId" });
            DropIndex("dbo.Payments", new[] { "UserId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Schedules", new[] { "BusId" });
            DropIndex("dbo.Schedules", new[] { "RouteId" });
            DropIndex("dbo.Routes", new[] { "BusStopId" });
            DropIndex("dbo.Routes", new[] { "BusId" });
            DropIndex("dbo.Bookings", new[] { "ScheduleId" });
            DropIndex("dbo.Bookings", new[] { "RouteId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropTable("dbo.Payments");
            DropTable("dbo.Users");
            DropTable("dbo.Schedules");
            DropTable("dbo.BusStops");
            DropTable("dbo.BusInfoes");
            DropTable("dbo.Routes");
            DropTable("dbo.Bookings");
        }
    }
}
