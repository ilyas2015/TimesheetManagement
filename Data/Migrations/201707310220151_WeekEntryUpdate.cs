namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeekEntryUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TsWeekEntries", "Day1StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day1EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day2StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day2EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day3StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day3EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day4StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day4EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day5StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day5EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day6StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day6EndTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day7StartTime", c => c.DateTime());
            AddColumn("dbo.TsWeekEntries", "Day7EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TsWeekEntries", "Day7EndTime");
            DropColumn("dbo.TsWeekEntries", "Day7StartTime");
            DropColumn("dbo.TsWeekEntries", "Day6EndTime");
            DropColumn("dbo.TsWeekEntries", "Day6StartTime");
            DropColumn("dbo.TsWeekEntries", "Day5EndTime");
            DropColumn("dbo.TsWeekEntries", "Day5StartTime");
            DropColumn("dbo.TsWeekEntries", "Day4EndTime");
            DropColumn("dbo.TsWeekEntries", "Day4StartTime");
            DropColumn("dbo.TsWeekEntries", "Day3EndTime");
            DropColumn("dbo.TsWeekEntries", "Day3StartTime");
            DropColumn("dbo.TsWeekEntries", "Day2EndTime");
            DropColumn("dbo.TsWeekEntries", "Day2StartTime");
            DropColumn("dbo.TsWeekEntries", "Day1EndTime");
            DropColumn("dbo.TsWeekEntries", "Day1StartTime");
        }
    }
}
