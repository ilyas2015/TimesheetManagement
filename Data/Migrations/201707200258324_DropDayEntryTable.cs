namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropDayEntryTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TsDayEntries", "TsWeekEntryId", "dbo.TsWeekEntries");
            DropIndex("dbo.TsDayEntries", new[] { "TsWeekEntryId" });
            AddColumn("dbo.TsWeekEntries", "Day1", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day1Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day2", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day2Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day3", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day3Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day4", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day4Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day5", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day5Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day6", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day6Hours", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TsWeekEntries", "Day7", c => c.DateTime(nullable: false));
            AddColumn("dbo.TsWeekEntries", "Day7Hours", c => c.Time(nullable: false, precision: 7));
            DropTable("dbo.TsDayEntries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TsDayEntries",
                c => new
                    {
                        TsDayEntryId = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        TsWeekEntryId = c.Int(nullable: false),
                        Hours = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TsDayEntryId);
            
            DropColumn("dbo.TsWeekEntries", "Day7Hours");
            DropColumn("dbo.TsWeekEntries", "Day7");
            DropColumn("dbo.TsWeekEntries", "Day6Hours");
            DropColumn("dbo.TsWeekEntries", "Day6");
            DropColumn("dbo.TsWeekEntries", "Day5Hours");
            DropColumn("dbo.TsWeekEntries", "Day5");
            DropColumn("dbo.TsWeekEntries", "Day4Hours");
            DropColumn("dbo.TsWeekEntries", "Day4");
            DropColumn("dbo.TsWeekEntries", "Day3Hours");
            DropColumn("dbo.TsWeekEntries", "Day3");
            DropColumn("dbo.TsWeekEntries", "Day2Hours");
            DropColumn("dbo.TsWeekEntries", "Day2");
            DropColumn("dbo.TsWeekEntries", "Day1Hours");
            DropColumn("dbo.TsWeekEntries", "Day1");
            CreateIndex("dbo.TsDayEntries", "TsWeekEntryId");
            AddForeignKey("dbo.TsDayEntries", "TsWeekEntryId", "dbo.TsWeekEntries", "TsWeekEntryId", cascadeDelete: true);
        }
    }
}
