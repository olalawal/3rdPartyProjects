namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatisticalSolutions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.clients", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.seminars", "StateProvince", c => c.String(nullable: false));
            AlterColumn("dbo.seminars", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.seminars", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.seminars", "Email", c => c.String());
            AlterColumn("dbo.seminars", "Country", c => c.String());
            AlterColumn("dbo.seminars", "StateProvince", c => c.String());
            DropColumn("dbo.clients", "IsActive");
        }
    }
}
