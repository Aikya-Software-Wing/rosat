namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeSemesterAndSubIdNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SubjectType", "Semester", c => c.Decimal(nullable: false, precision: 1, scale: 0, storeType: "numeric"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubjectType", "Semester", c => c.Decimal(precision: 1, scale: 0, storeType: "numeric"));
        }
    }
}
