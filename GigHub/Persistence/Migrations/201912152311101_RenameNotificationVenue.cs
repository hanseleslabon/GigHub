namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameNotificationVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "OriginalVenue", c => c.String());
            DropColumn("dbo.Notifications", "OriginalValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "OriginalValue", c => c.String());
            DropColumn("dbo.Notifications", "OriginalVenue");
        }
    }
}
