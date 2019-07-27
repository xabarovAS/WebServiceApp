namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Image");
        }
    }
}
