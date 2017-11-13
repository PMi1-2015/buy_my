namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Category_Id = c.Int(),
                        Busket_Id = c.Int(),
                        ShoppingList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Buskets", t => t.Busket_Id)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Busket_Id)
                .Index(t => t.ShoppingList_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ShoppingList_Id", "dbo.ShoppingLists");
            DropForeignKey("dbo.Products", "Busket_Id", "dbo.Buskets");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "ShoppingList_Id" });
            DropIndex("dbo.Products", new[] { "Busket_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropTable("dbo.ShoppingLists");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Buskets");
        }
    }
}
