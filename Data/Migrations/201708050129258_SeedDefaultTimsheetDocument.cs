namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedDefaultTimsheetDocument : DbMigration
    {
        public override void Up()
        {
            Guid docGuid = Guid.NewGuid();
            string newFileName = "TS_" + docGuid.ToString() + ".docx";

            Sql("INSERT INTO TsDocumentEntities (DocumentName, UserId, DocGuid, SavedName, ) VALUES ('Default_Template.docx', 'a0fa7d52-5a2e-4117-8f77-b6104aa707ab', '" + docGuid + "', '" + newFileName + "' )");

        }
        
        public override void Down()
        {
        }
    }
}
