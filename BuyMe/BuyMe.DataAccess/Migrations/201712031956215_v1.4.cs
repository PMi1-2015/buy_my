namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.ShoppingLists", "LastEditTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShoppingLists", "CreationTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingLists", "CreationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShoppingLists", "LastEditTime");
            DropColumn("dbo.Products", "Amount");
        }
    }
}
