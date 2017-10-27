namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeYearAndIsGPANotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SyllabusType", "Year", c => c.Decimal(nullable: false, precision: 4, scale: 0, storeType: "numeric"));
            AlterColumn("dbo.SyllabusType", "IsCGPA", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SyllabusType", "IsCGPA", c => c.Boolean());
            AlterColumn("dbo.SyllabusType", "Year", c => c.Decimal(precision: 4, scale: 0, storeType: "numeric"));
        }
    }
}
