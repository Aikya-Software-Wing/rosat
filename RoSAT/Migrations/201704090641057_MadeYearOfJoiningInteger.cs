namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeYearOfJoiningInteger : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "YearOfJoining", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "YearOfJoining", c => c.Decimal(nullable: false, precision: 4, scale: 0, storeType: "numeric"));
        }
    }
}
