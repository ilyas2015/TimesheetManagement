namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDocumentEntityClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TsDocumentEntities",
                c => new
                    {
                        TsDocumentEntityId = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        UserId = c.String(nullable: false),
                        DocGuid = c.Guid(nullable: false),
                        SavedName = c.String(),
                    })
                .PrimaryKey(t => t.TsDocumentEntityId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TsDocumentEntities");
        }
    }
}
