namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixonmajorminornew : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Student", name: "MajorQuota1_Id", newName: "MajorQuotas_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuota1_Id", newName: "MinorQuotas_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuota1_Id", newName: "IX_MajorQuotas_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuota1_Id", newName: "IX_MinorQuotas_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Student", name: "IX_MinorQuotas_Id", newName: "IX_MinorQuota1_Id");
            RenameIndex(table: "dbo.Student", name: "IX_MajorQuotas_Id", newName: "IX_MajorQuota1_Id");
            RenameColumn(table: "dbo.Student", name: "MinorQuotas_Id", newName: "MinorQuota1_Id");
            RenameColumn(table: "dbo.Student", name: "MajorQuotas_Id", newName: "MajorQuota1_Id");
        }
    }
}
