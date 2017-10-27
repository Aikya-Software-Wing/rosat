namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePositionInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Position", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Position", c => c.Decimal(nullable: false, precision: 6, scale: 0, storeType: "numeric"));
        }
    }
}
