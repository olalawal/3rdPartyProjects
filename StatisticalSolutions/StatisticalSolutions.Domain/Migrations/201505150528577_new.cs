namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.seminars", "Starttime", c => c.String());
            AddColumn("dbo.seminars", "Endtime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.seminars", "Endtime");
            DropColumn("dbo.seminars", "Starttime");
        }
    }
}
