namespace FirstBackEndProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCategoryParent : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "ParentId");
            AddForeignKey("dbo.Categories", "ParentId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentId" });
        }
    }
}
