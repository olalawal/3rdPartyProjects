namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedinstructors : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.instructors", "seminar_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.instructors", "seminar_id", c => c.Int());
        }
    }
}
