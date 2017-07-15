namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDefaultProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TsWeekTemplates", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TsWeekTemplates", "IsDefault");
        }
    }
}
