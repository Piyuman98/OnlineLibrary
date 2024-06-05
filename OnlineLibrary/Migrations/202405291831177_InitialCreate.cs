namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Author = c.String(nullable: false, maxLength: 255),
                        Genre = c.String(maxLength: 100),
                        PublishedYear = c.Int(nullable: false),
                        AvailableCopies = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.GuestID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        GuestID = c.Int(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.Guests", t => t.GuestID, cascadeDelete: true)
                .Index(t => t.GuestID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        GuestID = c.Int(nullable: false),
                        ReservationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Guests", t => t.GuestID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.GuestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "GuestID", "dbo.Guests");
            DropForeignKey("dbo.Reservations", "BookID", "dbo.Books");
            DropForeignKey("dbo.Messages", "GuestID", "dbo.Guests");
            DropIndex("dbo.Reservations", new[] { "GuestID" });
            DropIndex("dbo.Reservations", new[] { "BookID" });
            DropIndex("dbo.Messages", new[] { "GuestID" });
            DropTable("dbo.Reservations");
            DropTable("dbo.Messages");
            DropTable("dbo.Guests");
            DropTable("dbo.Books");
            DropTable("dbo.Admins");
        }
    }
}
