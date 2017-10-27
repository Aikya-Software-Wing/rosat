namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePercentageNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.School", "PercentageMarks", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.School", "PercentageMarks", c => c.Boolean(nullable: true));
        }
    }
}
