namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeteringDevices", "Owner_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.MeteringDevices", "Owner_ID");
            AddForeignKey("dbo.MeteringDevices", "Owner_ID", "dbo.Employees", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MeteringDevices", "Owner_ID", "dbo.Employees");
            DropIndex("dbo.MeteringDevices", new[] { "Owner_ID" });
            DropColumn("dbo.MeteringDevices", "Owner_ID");
        }
    }
}
