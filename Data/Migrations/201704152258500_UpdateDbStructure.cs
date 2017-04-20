namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbStructure : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TsEntries", "WeekTemplate_TsWeekTemplateId", "dbo.TsWeekTemplates");
            DropForeignKey("dbo.TsDayEntries", "TsEntryId", "dbo.TsEntries");
            DropIndex("dbo.TsDayEntries", new[] { "TsEntryId" });
            DropIndex("dbo.TsEntries", new[] { "WeekTemplate_TsWeekTemplateId" });
            CreateTable(
                "dbo.TsWeekEntries",
                c => new
                    {
                        TsWeekEntryId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TsWeekEntryId);
            
            AddColumn("dbo.TsDayEntries", "TsWeekEntryId", c => c.Int(nullable: false));
            CreateIndex("dbo.TsDayEntries", "TsWeekEntryId");
            AddForeignKey("dbo.TsDayEntries", "TsWeekEntryId", "dbo.TsWeekEntries", "TsWeekEntryId", cascadeDelete: true);
            DropColumn("dbo.TsDayEntries", "TsEntryId");
            DropTable("dbo.TsEntries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TsEntries",
                c => new
                    {
                        TsEntryId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        WeekTemplateId = c.Int(nullable: false),
                        TotalHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(nullable: false),
                        WeekTemplate_TsWeekTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TsEntryId);
            
            AddColumn("dbo.TsDayEntries", "TsEntryId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TsDayEntries", "TsWeekEntryId", "dbo.TsWeekEntries");
            DropIndex("dbo.TsDayEntries", new[] { "TsWeekEntryId" });
            DropColumn("dbo.TsDayEntries", "TsWeekEntryId");
            DropTable("dbo.TsWeekEntries");
            CreateIndex("dbo.TsEntries", "WeekTemplate_TsWeekTemplateId");
            CreateIndex("dbo.TsDayEntries", "TsEntryId");
            AddForeignKey("dbo.TsDayEntries", "TsEntryId", "dbo.TsEntries", "TsEntryId", cascadeDelete: true);
            AddForeignKey("dbo.TsEntries", "WeekTemplate_TsWeekTemplateId", "dbo.TsWeekTemplates", "TsWeekTemplateId", cascadeDelete: true);
        }
    }
}
