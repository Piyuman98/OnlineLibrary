namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMessageFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "GuestID", "dbo.Guests");
            DropIndex("dbo.Messages", new[] { "GuestID" });
            AddColumn("dbo.Messages", "Content", c => c.String());
            AddColumn("dbo.Messages", "Name", c => c.String());
            AddColumn("dbo.Messages", "Email", c => c.String());
            AddColumn("dbo.Messages", "DateSent", c => c.DateTime(nullable: false));
            DropColumn("dbo.Messages", "GuestID");
            DropColumn("dbo.Messages", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Messages", "GuestID", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "DateSent");
            DropColumn("dbo.Messages", "Content");
            DropColumn("dbo.Messages", "Email");
            DropColumn("dbo.Messages", "Name");
            CreateIndex("dbo.Messages", "GuestID");
            AddForeignKey("dbo.Messages", "GuestID", "dbo.Guests", "GuestID", cascadeDelete: true);
        }
    }
}
