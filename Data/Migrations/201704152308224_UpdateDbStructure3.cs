namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbStructure3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TsWeekEntries", "TsEntry_TsEntryId", "dbo.TsEntries");
            DropIndex("dbo.TsWeekEntries", new[] { "TsEntry_TsEntryId" });
            RenameColumn(table: "dbo.TsWeekEntries", name: "TsEntry_TsEntryId", newName: "TsEntryId");
            AlterColumn("dbo.TsWeekEntries", "TsEntryId", c => c.Int(nullable: false));
            CreateIndex("dbo.TsWeekEntries", "TsEntryId");
            AddForeignKey("dbo.TsWeekEntries", "TsEntryId", "dbo.TsEntries", "TsEntryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TsWeekEntries", "TsEntryId", "dbo.TsEntries");
            DropIndex("dbo.TsWeekEntries", new[] { "TsEntryId" });
            AlterColumn("dbo.TsWeekEntries", "TsEntryId", c => c.Int());
            RenameColumn(table: "dbo.TsWeekEntries", name: "TsEntryId", newName: "TsEntry_TsEntryId");
            CreateIndex("dbo.TsWeekEntries", "TsEntry_TsEntryId");
            AddForeignKey("dbo.TsWeekEntries", "TsEntry_TsEntryId", "dbo.TsEntries", "TsEntryId");
        }
    }
}
