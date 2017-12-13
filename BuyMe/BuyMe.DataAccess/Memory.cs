using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.DataAccess
{
    public static class Memory
    {
        public static ShoppingListDbContext Db { get; set; } = new ShoppingListDbContext();
    }
}
