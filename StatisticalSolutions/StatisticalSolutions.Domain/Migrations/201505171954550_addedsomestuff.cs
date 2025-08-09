namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsomestuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.instructors", "ImageName", c => c.String());
            AddColumn("dbo.instructors", "ImagePath", c => c.String());
            AddColumn("dbo.instructors", "DetailsHtml", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.instructors", "DetailsHtml");
            DropColumn("dbo.instructors", "ImagePath");
            DropColumn("dbo.instructors", "ImageName");
        }
    }
}
