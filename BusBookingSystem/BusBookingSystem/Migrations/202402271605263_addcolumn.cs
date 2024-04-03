namespace BusBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentMode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PaymentMode");
        }
    }
}
