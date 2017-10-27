namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undorename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MajorQuota", newName: "MajorQuotas");
            RenameTable(name: "dbo.MinorQuota", newName: "MinorQuotas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MinorQuotas", newName: "MinorQuota");
            RenameTable(name: "dbo.MajorQuotas", newName: "MajorQuota");
        }
    }
}
