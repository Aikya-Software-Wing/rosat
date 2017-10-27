namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cositasked : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Student", name: "MajorQuotas_Id", newName: "MajorQuota_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuotas_Id", newName: "MinorQuota_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuotas_Id", newName: "IX_MajorQuota_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuotas_Id", newName: "IX_MinorQuota_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuota_Id", newName: "IX_MinorQuotas_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuota_Id", newName: "IX_MajorQuotas_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuota_Id", newName: "MinorQuotas_Id");
            RenameColumn(table: "dbo.Student", name: "MajorQuota_Id", newName: "MajorQuotas_Id");
        }
    }
}
