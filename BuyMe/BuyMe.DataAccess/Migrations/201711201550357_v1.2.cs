namespace BuyMe.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingLists", "LastEditTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShoppingLists", "ReminderTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShoppingLists", "ReminderTime");
            DropColumn("dbo.ShoppingLists", "LastEditTime");
        }
    }
}
