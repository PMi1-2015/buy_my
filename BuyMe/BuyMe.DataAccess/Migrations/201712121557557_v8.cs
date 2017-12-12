namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Busket_Id", "dbo.Buskets");
            DropForeignKey("dbo.Products", "ShoppingList_Id", "dbo.ShoppingLists");
            DropIndex("dbo.Products", new[] { "Busket_Id" });
            DropIndex("dbo.Products", new[] { "ShoppingList_Id" });
            DropPrimaryKey("dbo.Products");
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Product_Name = c.String(maxLength: 128),
                        ShoppingList_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Name)
                .ForeignKey("dbo.ShoppingLists", t => t.ShoppingList_Id)
                .Index(t => t.Product_Name)
                .Index(t => t.ShoppingList_Id);
            
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Products", "Name");
            DropColumn("dbo.Products", "Id");
            DropColumn("dbo.Products", "Amount");
            DropColumn("dbo.Products", "Busket_Id");
            DropColumn("dbo.Products", "ShoppingList_Id");
            DropTable("dbo.Buskets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Buskets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ShoppingList_Id", c => c.Int());
            AddColumn("dbo.Products", "Busket_Id", c => c.Int());
            AddColumn("dbo.Products", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "ShoppingList_Id", "dbo.ShoppingLists");
            DropForeignKey("dbo.Orders", "Product_Name", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ShoppingList_Id" });
            DropIndex("dbo.Orders", new[] { "Product_Name" });
            DropPrimaryKey("dbo.Products");
            AlterColumn("dbo.Products", "Name", c => c.String());
            DropTable("dbo.Orders");
            AddPrimaryKey("dbo.Products", "Id");
            CreateIndex("dbo.Products", "ShoppingList_Id");
            CreateIndex("dbo.Products", "Busket_Id");
            AddForeignKey("dbo.Products", "ShoppingList_Id", "dbo.ShoppingLists", "Id");
            AddForeignKey("dbo.Products", "Busket_Id", "dbo.Buskets", "Id");
        }
    }
}
