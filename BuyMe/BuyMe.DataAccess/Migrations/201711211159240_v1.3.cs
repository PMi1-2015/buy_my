namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "ImagePath");
        }
    }
}
