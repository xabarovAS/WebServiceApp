namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StatusTasksEmployees", name: "EmployeеID_ID", newName: "EmployeеID");
            RenameIndex(table: "dbo.StatusTasksEmployees", name: "IX_EmployeеID_ID", newName: "IX_EmployeеID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StatusTasksEmployees", name: "IX_EmployeеID", newName: "IX_EmployeеID_ID");
            RenameColumn(table: "dbo.StatusTasksEmployees", name: "EmployeеID", newName: "EmployeеID_ID");
        }
    }
}
