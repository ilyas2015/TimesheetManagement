namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbStructure2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TsEntries",
                c => new
                    {
                        TsEntryId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        TotalHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TsEntryId);
            
            AddColumn("dbo.TsWeekEntries", "TsEntry_TsEntryId", c => c.Int());
            CreateIndex("dbo.TsWeekEntries", "TsEntry_TsEntryId");
            AddForeignKey("dbo.TsWeekEntries", "TsEntry_TsEntryId", "dbo.TsEntries", "TsEntryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TsWeekEntries", "TsEntry_TsEntryId", "dbo.TsEntries");
            DropIndex("dbo.TsWeekEntries", new[] { "TsEntry_TsEntryId" });
            DropColumn("dbo.TsWeekEntries", "TsEntry_TsEntryId");
            DropTable("dbo.TsEntries");
        }
    }
}
