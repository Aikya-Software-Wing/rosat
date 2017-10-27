namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSemester : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "Semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "Semester");
        }
    }
}
