namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingAdminDoc : DbMigration
    {
        public override void Up()
        {
            Sql("Update TsDocumentEntities SET SystemDefault = 1 WHERE UserId = 'a0fa7d52-5a2e-4117-8f77-b6104aa707ab'");
        }
        
        public override void Down()
        {
        }
    }
}
