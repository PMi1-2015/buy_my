using BuyMe.Models;
using System.Data.Entity;

namespace BuyMe.DataAccess
{
    public partial class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext()
            : base("ShoppingList")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Busket> Buskets { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
    }
}
