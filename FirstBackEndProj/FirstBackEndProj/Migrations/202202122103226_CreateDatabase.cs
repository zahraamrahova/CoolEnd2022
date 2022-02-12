namespace FirstBackEndProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthPermissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthGroupId = c.Int(nullable: false),
                        AuthActionId = c.Int(nullable: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                        CanEdit = c.Boolean(nullable: false),
                        CanView = c.Boolean(nullable: false),
                        OnlyOwner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthActions", t => t.AuthActionId, cascadeDelete: true)
                .ForeignKey("dbo.AuthGroups", t => t.AuthGroupId, cascadeDelete: true)
                .Index(t => t.AuthGroupId)
                .Index(t => t.AuthActionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthGroupId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(maxLength: 200),
                        Token = c.String(maxLength: 200),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuthGroups", t => t.AuthGroupId, cascadeDelete: true)
                .Index(t => t.AuthGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "AuthGroupId", "dbo.AuthGroups");
            DropForeignKey("dbo.AuthPermissions", "AuthGroupId", "dbo.AuthGroups");
            DropForeignKey("dbo.AuthPermissions", "AuthActionId", "dbo.AuthActions");
            DropIndex("dbo.Users", new[] { "AuthGroupId" });
            DropIndex("dbo.AuthPermissions", new[] { "AuthActionId" });
            DropIndex("dbo.AuthPermissions", new[] { "AuthGroupId" });
            DropTable("dbo.Users");
            DropTable("dbo.AuthPermissions");
            DropTable("dbo.AuthGroups");
            DropTable("dbo.AuthActions");
        }
    }
}
