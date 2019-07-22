namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusTasksEmployees", "EmployeеID_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.StatusTasksEmployees", "EmployeеID_ID");
            AddForeignKey("dbo.StatusTasksEmployees", "EmployeеID_ID", "dbo.Employees", "ID", cascadeDelete: true);
            DropColumn("dbo.StatusTasksEmployees", "EmployeеID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StatusTasksEmployees", "EmployeеID", c => c.Int(nullable: false));
            DropForeignKey("dbo.StatusTasksEmployees", "EmployeеID_ID", "dbo.Employees");
            DropIndex("dbo.StatusTasksEmployees", new[] { "EmployeеID_ID" });
            DropColumn("dbo.StatusTasksEmployees", "EmployeеID_ID");
        }
    }
}
