namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeDurationInteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobProjects", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobProjects", "Duration", c => c.Decimal(nullable: false, precision: 3, scale: 0, storeType: "numeric"));
        }
    }
}
