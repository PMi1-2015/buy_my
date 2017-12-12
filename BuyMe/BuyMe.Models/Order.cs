using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual ShoppingList ShoppingList { get; set;}
        public double Amount { get; set; }
    }
}
