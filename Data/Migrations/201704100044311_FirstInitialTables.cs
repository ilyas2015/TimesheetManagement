namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstInitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TsDayEntries",
                c => new
                    {
                        TsDayEntryId = c.Int(nullable: false, identity: true),
                        EventDate = c.DateTime(nullable: false),
                        TsEntryId = c.Int(nullable: false),
                        Hours = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.TsDayEntryId)
                .ForeignKey("dbo.TsEntries", t => t.TsEntryId, cascadeDelete: true)
                .Index(t => t.TsEntryId);
            
            CreateTable(
                "dbo.TsEntries",
                c => new
                    {
                        TsEntryId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        WeekTemplateId = c.Int(nullable: false),
                        TotalHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(nullable: false),
                        WeekTemplate_TsWeekTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TsEntryId)
                .ForeignKey("dbo.TsWeekTemplates", t => t.WeekTemplate_TsWeekTemplateId, cascadeDelete: true)
                .Index(t => t.WeekTemplate_TsWeekTemplateId);
            
            CreateTable(
                "dbo.TsWeekTemplates",
                c => new
                    {
                        TsWeekTemplateId = c.Int(nullable: false, identity: true),
                        TemplateName = c.String(),
                        FirstDayOfWeek = c.Int(nullable: false),
                        HoursInDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FillDay1 = c.Boolean(nullable: false),
                        FillDay2 = c.Boolean(nullable: false),
                        FillDay3 = c.Boolean(nullable: false),
                        FillDay4 = c.Boolean(nullable: false),
                        FillDay5 = c.Boolean(nullable: false),
                        FillDay6 = c.Boolean(nullable: false),
                        FillDay7 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TsWeekTemplateId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TsDayEntries", "TsEntryId", "dbo.TsEntries");
            DropForeignKey("dbo.TsEntries", "WeekTemplate_TsWeekTemplateId", "dbo.TsWeekTemplates");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TsEntries", new[] { "WeekTemplate_TsWeekTemplateId" });
            DropIndex("dbo.TsDayEntries", new[] { "TsEntryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TsWeekTemplates");
            DropTable("dbo.TsEntries");
            DropTable("dbo.TsDayEntries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
