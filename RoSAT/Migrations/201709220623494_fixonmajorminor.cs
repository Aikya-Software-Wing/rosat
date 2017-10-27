namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixonmajorminor : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Student", name: "MajorQuota_Id", newName: "MajorQuota1_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuota_Id", newName: "MinorQuota1_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuota_Id", newName: "IX_MajorQuota1_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuota_Id", newName: "IX_MinorQuota1_Id");
            AddColumn("dbo.Student", "MajorQuota", c => c.Int(nullable: false));
            AddColumn("dbo.Student", "MinorQuota", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "MinorQuota");
            DropColumn("dbo.Student", "MajorQuota");
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuota1_Id", newName: "IX_MinorQuota_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuota1_Id", newName: "IX_MajorQuota_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuota1_Id", newName: "MinorQuota_Id");
            RenameColumn(table: "dbo.Student", name: "MajorQuota1_Id", newName: "MajorQuota_Id");
        }
    }
}
