namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeofname : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MajorQuotas", newName: "MajorQuota");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MajorQuota", newName: "MajorQuotas");
        }
    }
}
