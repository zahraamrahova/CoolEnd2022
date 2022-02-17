namespace FirstBackEndProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
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
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(storeType: "ntext"),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Aviability = c.Boolean(nullable: false),
                        Discount = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Adress = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductSizeProducts",
                c => new
                    {
                        ProductSize_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductSize_Id, t.Product_Id })
                .ForeignKey("dbo.ProductSizes", t => t.ProductSize_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ProductSize_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSizeProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductSizeProducts", "ProductSize_Id", "dbo.ProductSizes");
            DropForeignKey("dbo.ProductPhotoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Users", "AuthGroupId", "dbo.AuthGroups");
            DropForeignKey("dbo.AuthPermissions", "AuthGroupId", "dbo.AuthGroups");
            DropForeignKey("dbo.AuthPermissions", "AuthActionId", "dbo.AuthActions");
            DropIndex("dbo.ProductSizeProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductSizeProducts", new[] { "ProductSize_Id" });
            DropIndex("dbo.ProductPhotoes", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Users", new[] { "AuthGroupId" });
            DropIndex("dbo.AuthPermissions", new[] { "AuthActionId" });
            DropIndex("dbo.AuthPermissions", new[] { "AuthGroupId" });
            DropTable("dbo.ProductSizeProducts");
            DropTable("dbo.ProductSizes");
            DropTable("dbo.ProductPhotoes");
            DropTable("dbo.Clients");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
            DropTable("dbo.Users");
            DropTable("dbo.AuthPermissions");
            DropTable("dbo.AuthGroups");
            DropTable("dbo.AuthActions");
        }
    }
}
