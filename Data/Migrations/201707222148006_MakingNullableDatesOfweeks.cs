namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakingNullableDatesOfweeks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TsWeekEntries", "Day1", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day2", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day3", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day4", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day5", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day6", c => c.DateTime());
            AlterColumn("dbo.TsWeekEntries", "Day7", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TsWeekEntries", "Day7", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day6", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day5", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day4", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day3", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day2", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TsWeekEntries", "Day1", c => c.DateTime(nullable: false));
        }
    }
}
