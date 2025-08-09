namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registerchange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.clients",
                c => new
                    {
                        client_id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        StateProvince = c.String(),
                        ZipPostalCode = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.client_id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.instructors",
                c => new
                    {
                        instructor_id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        StateProvince = c.String(),
                        ZipPostalCode = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ImageName = c.String(),
                        ImagePath = c.String(),
                        DetailsHtml = c.String(),
                    })
                .PrimaryKey(t => t.instructor_id);
            
            CreateTable(
                "dbo.seminars",
                c => new
                    {
                        seminar_id = c.Int(nullable: false, identity: true),
                        instructor_id = c.Int(),
                        TitleHtml = c.String(nullable: false),
                        EventDetailsHtml = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Enddate = c.DateTime(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(),
                        StateProvince = c.String(nullable: false),
                        ZipPostalCode = c.String(),
                        Country = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Fax = c.String(),
                        ContactEmail = c.String(),
                        ContactPhone = c.String(),
                        ContactWebsite = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Starttime = c.String(),
                        Endtime = c.String(),
                        EarlyBirdPrice = c.String(),
                        NormalPrice = c.String(),
                    })
                .PrimaryKey(t => t.seminar_id)
                .ForeignKey("dbo.instructors", t => t.instructor_id)
                .Index(t => t.instructor_id);
            
            CreateTable(
                "dbo.registrations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        client_id = c.Int(),
                        student_id = c.Int(nullable: false),
                        seminar_id = c.Int(nullable: false),
                        Paid = c.Boolean(),
                        Attendend = c.Boolean(),
                        Attenddate = c.DateTime(),
                        Registerdate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.clients", t => t.client_id)
                .ForeignKey("dbo.seminars", t => t.seminar_id, cascadeDelete: true)
                .ForeignKey("dbo.students", t => t.student_id, cascadeDelete: true)
                .Index(t => t.client_id)
                .Index(t => t.student_id)
                .Index(t => t.seminar_id);
            
            CreateTable(
                "dbo.students",
                c => new
                    {
                        student_id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        StateProvince = c.String(),
                        ZipPostalCode = c.String(),
                        Country = c.String(),
                        Description = c.String(),
                        Email = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Fax = c.String(),
                        BankAccountNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.student_id);
            
            CreateTable(
                "dbo.messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        MessageDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.registrations", "student_id", "dbo.students");
            DropForeignKey("dbo.registrations", "seminar_id", "dbo.seminars");
            DropForeignKey("dbo.registrations", "client_id", "dbo.clients");
            DropForeignKey("dbo.seminars", "instructor_id", "dbo.instructors");
            DropIndex("dbo.registrations", new[] { "seminar_id" });
            DropIndex("dbo.registrations", new[] { "student_id" });
            DropIndex("dbo.registrations", new[] { "client_id" });
            DropIndex("dbo.seminars", new[] { "instructor_id" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.messages");
            DropTable("dbo.students");
            DropTable("dbo.registrations");
            DropTable("dbo.seminars");
            DropTable("dbo.instructors");
            DropTable("dbo.Countries");
            DropTable("dbo.clients");
        }
    }
}
