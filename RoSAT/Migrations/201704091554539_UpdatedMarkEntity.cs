namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedMarkEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Marks", "Sem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Marks", "Sem", c => c.Decimal(precision: 1, scale: 0, storeType: "numeric"));
        }
    }
}
