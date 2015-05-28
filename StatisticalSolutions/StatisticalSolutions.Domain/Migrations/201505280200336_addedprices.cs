namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedprices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.seminars", "EarlyBirdPrice", c => c.String());
            AddColumn("dbo.seminars", "NormalPrice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.seminars", "NormalPrice");
            DropColumn("dbo.seminars", "EarlyBirdPrice");
        }
    }
}
