namespace WebServiceApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MeteringDevices", name: "Owner_ID", newName: "OwnerID");
            RenameIndex(table: "dbo.MeteringDevices", name: "IX_Owner_ID", newName: "IX_OwnerID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MeteringDevices", name: "IX_OwnerID", newName: "IX_Owner_ID");
            RenameColumn(table: "dbo.MeteringDevices", name: "OwnerID", newName: "Owner_ID");
        }
    }
}
