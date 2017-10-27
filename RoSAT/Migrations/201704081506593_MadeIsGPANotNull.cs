namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeIsGPANotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.School", "IsGPA", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.School", "IsGPA", c => c.Boolean());
        }
    }
}
