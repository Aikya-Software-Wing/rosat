namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FixedIncorrectDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.School", "PercentageMarks", c => c.Decimal(nullable: false, precision: 5, scale: 2));
        }

        public override void Down()
        {
            AlterColumn("dbo.School", "PercentageMarks", c => c.Boolean(nullable: false));
        }
    }
}
