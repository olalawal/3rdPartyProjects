namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.instructors",
                c => new
                    {
                        instructor_id = c.Int(nullable: false, identity: true),
                        seminar_id = c.Int(),
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
                    })
                .PrimaryKey(t => t.instructor_id);
            
            AddColumn("dbo.seminars", "instructor_id", c => c.Int());
            CreateIndex("dbo.seminars", "instructor_id");
            AddForeignKey("dbo.seminars", "instructor_id", "dbo.instructors", "instructor_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.seminars", "instructor_id", "dbo.instructors");
            DropIndex("dbo.seminars", new[] { "instructor_id" });
            DropColumn("dbo.seminars", "instructor_id");
            DropTable("dbo.instructors");
        }
    }
}
