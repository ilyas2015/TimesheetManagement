namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSystemDefaultField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TsDocumentEntities", "SystemDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TsDocumentEntities", "SystemDefault");
        }
    }
}
