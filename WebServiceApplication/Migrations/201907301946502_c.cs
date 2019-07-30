namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeesPositions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Period = c.DateTime(nullable: false),
                        EmployeеID = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.EmployeеID, cascadeDelete: true)
                .Index(t => t.EmployeеID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeesPositions", "EmployeеID", "dbo.Employees");
            DropIndex("dbo.EmployeesPositions", new[] { "EmployeеID" });
            DropTable("dbo.EmployeesPositions");
        }
    }
}
