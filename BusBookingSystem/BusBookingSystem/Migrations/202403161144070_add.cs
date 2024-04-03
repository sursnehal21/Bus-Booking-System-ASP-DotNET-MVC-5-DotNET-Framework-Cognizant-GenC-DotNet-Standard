namespace BusBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "SourceBusStopId", c => c.String());
            AddColumn("dbo.Schedules", "DestinationBusStopId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "DestinationBusStopId");
            DropColumn("dbo.Schedules", "SourceBusStopId");
        }
    }
}
