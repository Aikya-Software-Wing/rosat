namespace RoSAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajorQuotas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MajorQuotas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MinorQuota",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Student", "MajorQuotas_Id", c => c.Int());
            AddColumn("dbo.Student", "MinorQuotas_Id", c => c.Int());
            CreateIndex("dbo.Student", "MajorQuotas_Id");
            CreateIndex("dbo.Student", "MinorQuotas_Id");
            AddForeignKey("dbo.Student", "MajorQuotas_Id", "dbo.MajorQuotas", "Id");
            AddForeignKey("dbo.Student", "MinorQuotas_Id", "dbo.MinorQuota", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "MinorQuotas_Id", "dbo.MinorQuota");
            DropForeignKey("dbo.Student", "MajorQuotas_Id", "dbo.MajorQuotas");
            DropIndex("dbo.Student", new[] { "MinorQuotas_Id" });
            DropIndex("dbo.Student", new[] { "MajorQuotas_Id" });
            DropColumn("dbo.Student", "MinorQuotas_Id");
            DropColumn("dbo.Student", "MajorQuotas_Id");
            DropTable("dbo.MinorQuota");
            DropTable("dbo.MajorQuotas");
        }
    }
}
