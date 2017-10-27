namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeEmailNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "EmailId", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "EmailId", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
