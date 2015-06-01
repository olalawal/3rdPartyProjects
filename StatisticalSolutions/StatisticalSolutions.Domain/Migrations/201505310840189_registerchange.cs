namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registerchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.clients", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.clients", "Name", c => c.String(nullable: false));
        }
    }
}
