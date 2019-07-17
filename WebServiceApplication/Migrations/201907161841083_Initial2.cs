namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeteringDevices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FactoryNumber = c.String(),
                        IDMeteringDevice = c.Guid(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StatusTasksEmployees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusTask = c.Int(nullable: false),
                        Employeе_ID = c.Int(nullable: false),
                        TasksEmployee_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employees", t => t.Employeе_ID, cascadeDelete: true)
                .ForeignKey("dbo.TasksEmployees", t => t.TasksEmployee_ID, cascadeDelete: true)
                .Index(t => t.Employeе_ID)
                .Index(t => t.TasksEmployee_ID);
            
            CreateTable(
                "dbo.TasksEmployees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Employees", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "SecondName", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "ThirdName", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "IDEmployeе", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Employees", "FullName", c => c.String());
            DropColumn("dbo.Employees", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Name", c => c.String());
            DropForeignKey("dbo.StatusTasksEmployees", "TasksEmployee_ID", "dbo.TasksEmployees");
            DropForeignKey("dbo.StatusTasksEmployees", "Employeе_ID", "dbo.Employees");
            DropIndex("dbo.StatusTasksEmployees", new[] { "TasksEmployee_ID" });
            DropIndex("dbo.StatusTasksEmployees", new[] { "Employeе_ID" });
            DropColumn("dbo.Employees", "FullName");
            DropColumn("dbo.Employees", "IDEmployeе");
            DropColumn("dbo.Employees", "ThirdName");
            DropColumn("dbo.Employees", "SecondName");
            DropColumn("dbo.Employees", "FirstName");
            DropTable("dbo.TasksEmployees");
            DropTable("dbo.StatusTasksEmployees");
            DropTable("dbo.MeteringDevices");
        }
    }
}
