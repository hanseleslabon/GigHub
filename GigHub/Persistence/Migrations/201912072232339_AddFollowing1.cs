namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowing1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FollowerID", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Followings", "FollowerID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FollowerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Followings", "FollowerID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Followings", "FolloweeId", "dbo.AspNetUsers", "Id");
        }
    }
}
