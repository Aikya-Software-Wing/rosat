namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigratuionAfterMergeConflict : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "EmailId", c => c.String(maxLength: 255));
            AlterColumn("dbo.Student", "YearOfJoining", c => c.Int(nullable: true));
            AlterColumn("dbo.JobProjects", "Duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobProjects", "Duration", c => c.Decimal(nullable: false, precision: 3, scale: 0, storeType: "numeric"));
            AlterColumn("dbo.Student", "YearOfJoining", c => c.Decimal(nullable: false, precision: 4, scale: 0, storeType: "numeric"));
            AlterColumn("dbo.Student", "EmailId", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
