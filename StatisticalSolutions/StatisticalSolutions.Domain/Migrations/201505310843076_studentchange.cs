namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.students", "Address1", c => c.String());
            AlterColumn("dbo.students", "City", c => c.String());
            AlterColumn("dbo.students", "StateProvince", c => c.String());
            AlterColumn("dbo.students", "Country", c => c.String());
            AlterColumn("dbo.students", "BankAccountNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.students", "BankAccountNumber", c => c.String(nullable: false));
            AlterColumn("dbo.students", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.students", "StateProvince", c => c.String(nullable: false));
            AlterColumn("dbo.students", "City", c => c.String(nullable: false));
            AlterColumn("dbo.students", "Address1", c => c.String(nullable: false));
        }
    }
}
