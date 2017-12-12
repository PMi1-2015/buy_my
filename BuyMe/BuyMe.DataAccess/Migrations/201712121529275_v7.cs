namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "Description");
        }
    }
}
